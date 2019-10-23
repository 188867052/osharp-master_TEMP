using System;
using Microsoft.Extensions.Logging;
using NLog;
using OSharp.Data;

namespace OSharp.NLog
{
    /// <summary>
    /// NLog日志记录
    /// </summary>
    public class NLogLogger : Microsoft.Extensions.Logging.ILogger
    {
        private readonly Logger _log;

        /// <summary>
        /// 初始化一个<see cref="NLogLogger"/>类型的新实例
        /// </summary>
        public NLogLogger(string name)
        {
            this._log = LogManager.GetLogger(name);
        }

        /// <summary>Writes a log entry.</summary>
        /// <param name="logLevel">日志级别，将按这个级别写不同的日志</param>
        /// <param name="eventId">事件编号.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        [Obsolete]
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            Check.NotNull(formatter, nameof(formatter));
            string message = null;
            if (formatter != null)
            {
                message = formatter(state, exception);
            }

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case Microsoft.Extensions.Logging.LogLevel.Trace:
                        this._log.Trace(message);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Debug:
                        this._log.Debug(message);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Information:
                        this._log.Info(message);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Warning:
                        this._log.Warn(message);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Error:
                        this._log.Error(message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Critical:
                        this._log.Fatal(message, exception);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.None:
                        break;
                    default:
                        this._log.Warn($"遇到未知的日志级别 {logLevel}, 使用Info级别写入日志。");
                        this._log.Info(message, exception);
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return this._log.IsTraceEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return this._log.IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return this._log.IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return this._log.IsWarnEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return this._log.IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return this._log.IsFatalEnabled;

                case Microsoft.Extensions.Logging.LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
