// -----------------------------------------------------------------------
//  <copyright file="AsyncBarrier.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-31 23:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace OSharp.Threading.Asyncs
{
    public class AsyncBarrier
    {
        private readonly int _participantCount;
        private int _remainingParticipants;
        private TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public AsyncBarrier(int participantCount)
        {
            if (participantCount <= 0)
            {
                throw new ArgumentOutOfRangeException("participantCount");
            }

            this._remainingParticipants = this._participantCount = participantCount;
        }

        public Task SignalAndWait()
        {
            var tcs = this._tcs;
            if (Interlocked.Decrement(ref this._remainingParticipants) == 0)
            {
                this._remainingParticipants = this._participantCount;
                this._tcs = new TaskCompletionSource<bool>();
                tcs.SetResult(true);
            }

            return tcs.Task;
        }

        public class AsyncBarrier1
        {
            private readonly int _participantCount;
            private int _remainingParticipants;
            private ConcurrentStack<TaskCompletionSource<bool>> _waiters;

            public AsyncBarrier1(int participantCount)
            {
                if (participantCount <= 0)
                {
                    throw new ArgumentOutOfRangeException("participantCount");
                }

                this._remainingParticipants = this._participantCount = participantCount;
                this._waiters = new ConcurrentStack<TaskCompletionSource<bool>>();
            }

            public Task SignalAndWait()
            {
                var tcs = new TaskCompletionSource<bool>();
                this._waiters.Push(tcs);
                if (Interlocked.Decrement(ref this._remainingParticipants) == 0)
                {
                    this._remainingParticipants = this._participantCount;
                    var waiters = this._waiters;
                    this._waiters = new ConcurrentStack<TaskCompletionSource<bool>>();
                    Parallel.ForEach(waiters, w => w.SetResult(true));
                }

                return tcs.Task;
            }
        }
    }
}