using Agile.Core.Identity.Dtos;
using Liuliu.Demo.Identity.Entities;
using OSharp.EventBuses;

namespace Agile.Core.Identity.Events
{
    /// <summary>
    /// 登录事件数据
    /// </summary>
    public class LoginEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 登录信息
        /// </summary>
        public LoginDto LoginDto { get; set; }

        /// <summary>
        /// 获取或设置 登录用户
        /// </summary>
        public User User { get; set; }
    }
}