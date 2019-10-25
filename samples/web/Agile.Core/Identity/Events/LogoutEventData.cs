using OSharp.EventBuses;

namespace Agile.Core.Identity.Events
{
    /// <summary>
    /// 用户退出事件数据
    /// </summary>
    public class LogoutEventData : EventDataBase
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int UserId { get; set; }
    }
}