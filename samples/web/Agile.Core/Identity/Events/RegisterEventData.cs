using Agile.Core.Identity.Dtos;
using Liuliu.Demo.Identity.Entities;
using OSharp.EventBuses;

namespace Agile.Core.Identity.Events
{
    /// <summary>
    /// 注册事件数据
    /// </summary>
    public class RegisterEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 注册信息
        /// </summary>
        public RegisterDto RegisterDto { get; set; }

        /// <summary>
        /// 获取或设置 注册用户
        /// </summary>
        public User User { get; set; }
    }
}