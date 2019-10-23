using System.Collections.Generic;
using OSharp.Data;
using OSharp.EventBuses;

namespace OSharp.Audits
{
    /// <summary>
    /// <see cref="AuditEntityEntry"/>事件源
    /// </summary>
    public class AuditEntityEventData : EventDataBase
    {
        /// <summary>
        /// 初始化一个<see cref="AuditEntityEventData"/>类型的新实例
        /// </summary>
        public AuditEntityEventData(IList<AuditEntityEntry> auditEntities)
        {
            Check.NotNull(auditEntities, nameof(auditEntities));

            this.AuditEntities = auditEntities;
        }

        /// <summary>
        /// 获取或设置 AuditData数据集合
        /// </summary>
        public IEnumerable<AuditEntityEntry> AuditEntities { get; }
    }
}