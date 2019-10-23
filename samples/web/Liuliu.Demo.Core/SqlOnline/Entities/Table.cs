using System.ComponentModel;

namespace Liuliu.Demo.Core.Release.Entities
{
    /// <summary>
    /// 表信息
    /// </summary>
    [Description("表信息")]
    public class Table : TableBase<int>
    {
        /// <summary>
        /// 表名称.
        /// </summary>
        public string Name { get; set; }
    }
}