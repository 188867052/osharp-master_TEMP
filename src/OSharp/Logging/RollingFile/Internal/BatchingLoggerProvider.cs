using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OSharp.Logging.RollingFile.Internal
{
// power by https://github.com/andrewlock/NetEscapades.Extensions.Logging
    public abstract class BatchingLoggerProvider : ILoggerProvider
    {
        private readonly List<LogMessageEntry> _currentBatch = new List<LogMessageEntry>();
        private readonly TimeSpan _interval;
        private readonly int? _queueSize;
        private readonly int? _batchSize;

        private BlockingCollection<LogMessageEntry> _messageQueue;
        private Task _outputTask;
        private CancellationTokenSource _cancellationTokenSource;

        protected BatchingLoggerProvider(IOptions<BatchingLoggerOptions> options)
        {
            // NOTE: Only IsEnabled is monitored

            var loggerOptions = options.Value;
            if (loggerOptions.BatchSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loggerOptions.BatchSize), $"{nameof(loggerOptions.BatchSize)} must be a positive number.");
            }

            if (loggerOptions.FlushPeriod <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(loggerOptions.FlushPeriod), $"{nameof(loggerOptions.FlushPeriod)} must be longer than zero.");
            }

            this._interval = loggerOptions.FlushPeriod;
            this._batchSize = loggerOptions.BatchSize;
            this._queueSize = loggerOptions.BackgroundQueueSize;

            this.Start();
        }

        protected abstract Task WriteMessagesAsync(IEnumerable<LogMessageEntry> messages, CancellationToken token);

        private async Task ProcessLogQueue(object state)
        {
            while (!this._cancellationTokenSource.IsCancellationRequested)
            {
                var limit = this._batchSize ?? int.MaxValue;

                while (limit > 0 && this._messageQueue.TryTake(out var message))
                {
                    this._currentBatch.Add(message);
                    limit--;
                }

                if (this._currentBatch.Count > 0)
                {
                    try
                    {
                        await this.WriteMessagesAsync(this._currentBatch, this._cancellationTokenSource.Token);
                    }
                    catch
                    {
                        // ignored
                    }

                    this._currentBatch.Clear();
                }

                await this.IntervalAsync(this._interval, this._cancellationTokenSource.Token);
            }
        }

        protected virtual Task IntervalAsync(TimeSpan interval, CancellationToken cancellationToken)
        {
            return Task.Delay(interval, cancellationToken);
        }

        internal void AddMessage(DateTimeOffset timestamp, string message)
        {
            if (!this._messageQueue.IsAddingCompleted)
            {
                try
                {
                    this._messageQueue.Add(new LogMessageEntry { Message = message, Timestamp = timestamp }, this._cancellationTokenSource.Token);
                }
                catch
                {
                    // cancellation token canceled or CompleteAdding called
                }
            }
        }

        private void Start()
        {
            this._messageQueue = this._queueSize == null ?
                new BlockingCollection<LogMessageEntry>(new ConcurrentQueue<LogMessageEntry>()) :
                new BlockingCollection<LogMessageEntry>(new ConcurrentQueue<LogMessageEntry>(), this._queueSize.Value);

            this._cancellationTokenSource = new CancellationTokenSource();
            this._outputTask = Task.Factory.StartNew<Task>(
                this.ProcessLogQueue,
                null,
                TaskCreationOptions.LongRunning);
        }

        private void Stop()
        {
            this._cancellationTokenSource.Cancel();
            this._messageQueue.CompleteAdding();

            try
            {
                this._outputTask.Wait(this._interval);
            }
            catch (TaskCanceledException)
            {
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException)
            {
            }
        }

        public void Dispose()
        {
            this.Stop();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new BatchingLogger(this, categoryName);
        }
    }
}