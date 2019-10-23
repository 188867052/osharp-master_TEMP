// -----------------------------------------------------------------------
//  <copyright file="AsyncAutoResetEvent.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2016 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2016-03-31 23:04</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSharp.Threading.Asyncs
{
    /// <summary>
    /// 异步自动重置事件
    /// </summary>
    public class AsyncAutoResetEvent
    {
        private static readonly Task Completed = Task.FromResult(true);
        private readonly Queue<TaskCompletionSource<bool>> _waits = new Queue<TaskCompletionSource<bool>>();
        private bool _signaled;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Task WaitAsync()
        {
            lock (this._waits)
            {
                if (this._signaled)
                {
                    this._signaled = false;
                    return Completed;
                }

                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                this._waits.Enqueue(tcs);
                return tcs.Task;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Set()
        {
            TaskCompletionSource<bool> toRelease = null;
            lock (this._waits)
            {
                if (this._waits.Count > 0)
                {
                    toRelease = this._waits.Dequeue();
                }
                else if (!this._signaled)
                {
                    this._signaled = true;
                }
            }

            if (toRelease != null)
            {
                toRelease.SetResult(true);
            }
        }
    }
}