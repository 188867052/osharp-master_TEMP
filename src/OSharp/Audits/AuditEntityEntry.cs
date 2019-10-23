using System.Collections.Generic;

namespace OSharp.Audits
{
    /// <summary>
    /// 实体审计信息
    /// </summary>
    public class AuditEntityEntry
    {
        /// <summary>
        /// 初始化一个<see cref="AuditEntityEntry"/>类型的新实例
        /// </summary>
        public AuditEntityEntry()
            : this(null, null, OperateType.Query)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AuditEntityEntry"/>类型的新实例
        /// </summary>
        public AuditEntityEntry(string name, string typeName, OperateType operateType)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.OperateType = operateType;
            this.PropertyEntries = new List<AuditPropertyEntry>();
        }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 获取或设置 数据编号
        /// </summary>
        public string EntityKey { get; set; }

        /// <summary>
        /// 获取或设置 操作类型
        /// </summary>
        public OperateType OperateType { get; set; }

        /// <summary>
        /// 获取或设置 操作实体属性集合
        /// </summary>
        public ICollection<AuditPropertyEntry> PropertyEntries { get; set; }

        /// <summary>
        /// 获取或设置 关联实体
        /// </summary>
        public object Entity { get; set; }
    }
}