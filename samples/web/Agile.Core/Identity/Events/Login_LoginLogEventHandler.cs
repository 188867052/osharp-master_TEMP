﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Agile.Core.Identity.Entities;
using Agile.Core.Identity.Events;
using OSharp.Entity;
using OSharp.EventBuses;

namespace Liuliu.Demo.Identity.Events
{
    /// <summary>
    /// 用户登录事件：登录日志
    /// </summary>
    public class LoginLoginLogEventHandler : EventHandlerBase<LoginEventData>
    {
        private readonly IRepository<LoginLog, Guid> _loginLogRepository;

        /// <summary>
        /// 初始化一个<see cref="LoginLoginLogEventHandler"/>类型的新实例
        /// </summary>
        public LoginLoginLogEventHandler(IRepository<LoginLog, Guid> loginLogRepository)
        {
            this._loginLogRepository = loginLogRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(LoginEventData eventData)
        {
            LoginLog log = new LoginLog()
            {
                Ip = eventData.LoginDto.Ip,
                UserAgent = eventData.LoginDto.UserAgent,
                UserId = eventData.User.Id,
            };
            this._loginLogRepository.Insert(log);
        }

        /// <summary>
        /// 异步事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        /// <returns>是否成功</returns>
        public override Task HandleAsync(LoginEventData eventData, CancellationToken cancelToken = default)
        {
            LoginLog log = new LoginLog()
            {
                Ip = eventData.LoginDto.Ip,
                UserAgent = eventData.LoginDto.UserAgent,
                UserId = eventData.User.Id,
            };
            return this._loginLogRepository.InsertAsync(log);
        }
    }
}