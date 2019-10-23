// -----------------------------------------------------------------------
//  <copyright file="AsyncReaderWriterLock.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-31 23:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSharp.Threading.Asyncs
{
    /// <summary>
    /// 异步读写锁
    /// </summary>
    public class AsyncReaderWriterLock
    {
        private readonly Task<Releaser> _readerReleaser;
        private readonly Queue<TaskCompletionSource<Releaser>> _waitingWriters = new Queue<TaskCompletionSource<Releaser>>();
        private readonly Task<Releaser> _writerReleaser;
        private int _readersWaiting;
        private int _status;
        private TaskCompletionSource<Releaser> _waitingReader =
            new TaskCompletionSource<Releaser>();

        public AsyncReaderWriterLock()
        {
            this._readerReleaser = Task.FromResult(new Releaser(this, false));
            this._writerReleaser = Task.FromResult(new Releaser(this, true));
        }

        public Task<Releaser> ReaderLockAsync()
        {
            lock (this._waitingWriters)
            {
                if (this._status >= 0 && this._waitingWriters.Count == 0)
                {
                    ++this._status;
                    return this._readerReleaser;
                }

                ++this._readersWaiting;
                return this._waitingReader.Task.ContinueWith(t => t.Result);
            }
        }

        public Task<Releaser> WriterLockAsync()
        {
            lock (this._waitingWriters)
            {
                if (this._status == 0)
                {
                    this._status = -1;
                    return this._writerReleaser;
                }
                else
                {
                    var waiter = new TaskCompletionSource<Releaser>();
                    this._waitingWriters.Enqueue(waiter);
                    return waiter.Task;
                }
            }
        }

        private void ReaderRelease()
        {
            TaskCompletionSource<Releaser> toWake = null;

            lock (this._waitingWriters)
            {
                --this._status;
                if (this._status == 0 && this._waitingWriters.Count > 0)
                {
                    this._status = -1;
                    toWake = this._waitingWriters.Dequeue();
                }
            }

            if (toWake != null)
            {
                toWake.SetResult(new Releaser(this, true));
            }
        }

        private void WriterRelease()
        {
            TaskCompletionSource<Releaser> toWake = null;
            bool toWakeIsWriter = false;

            lock (this._waitingWriters)
            {
                if (this._waitingWriters.Count > 0)
                {
                    toWake = this._waitingWriters.Dequeue();
                    toWakeIsWriter = true;
                }
                else if (this._readersWaiting > 0)
                {
                    toWake = this._waitingReader;
                    this._status = this._readersWaiting;
                    this._readersWaiting = 0;
                    this._waitingReader = new TaskCompletionSource<Releaser>();
                }
                else
                {
                    this._status = 0;
                }
            }

            if (toWake != null)
            {
                toWake.SetResult(new Releaser(this, toWakeIsWriter));
            }
        }

        public struct Releaser : IDisposable
        {
            private readonly AsyncReaderWriterLock _toRelease;
            private readonly bool _writer;

            internal Releaser(AsyncReaderWriterLock toRelease, bool writer)
            {
                this._toRelease = toRelease;
                this._writer = writer;
            }

            public void Dispose()
            {
                if (this._toRelease != null)
                {
                    if (this._writer)
                    {
                        this._toRelease.WriterRelease();
                    }
                    else
                    {
                        this._toRelease.ReaderRelease();
                    }
                }
            }
        }
    }
}