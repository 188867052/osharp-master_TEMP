using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Agile.Core.Identity.Entities;
using OSharp.Entity;
using OSharp.EventBuses;

namespace Agile.Core.Identity.Events
{
    /// <summary>
    /// 用户登出事件：登录日志
    /// </summary>
    public class LogoutLoginLogEventHandler : EventHandlerBase<LogoutEventData>
    {
        private readonly IRepository<LoginLog, Guid> _loginLogRepository;

        /// <summary>
        /// 初始化一个<see cref="LogoutLoginLogEventHandler"/>类型的新实例
        /// </summary>
        public LogoutLoginLogEventHandler(IRepository<LoginLog, Guid> loginLogRepository)
        {
            _loginLogRepository = loginLogRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(LogoutEventData eventData)
        {
            LoginLog log = _loginLogRepository.QueryAsNoTracking().LastOrDefault(m => m.UserId == eventData.UserId);
            if (log == null)
            {
                return;
            }

            log.LogoutTime = DateTime.Now;
            _loginLogRepository.Update(log);
        }

        /// <summary>
        /// 异步事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        /// <param name="cancelToken">异步取消标识</param>
        /// <returns>是否成功</returns>
        public override async Task HandleAsync(LogoutEventData eventData, CancellationToken cancelToken = default)
        {
            LoginLog log = _loginLogRepository.QueryAsNoTracking().LastOrDefault(m => m.UserId == eventData.UserId);
            if (log == null)
            {
                return;
            }

            log.LogoutTime = DateTime.Now;
            await _loginLogRepository.UpdateAsync(log);
        }
    }
}