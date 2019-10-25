using System.ComponentModel;

namespace Liuliu.Demo.Core.Release.Entities
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [Description("版本信息")]
    public class Versions : VersionsBase<int>
    {
        /// <summary>
        /// 版本名称.
        /// </summary>
        public string Name { get; set; }
    }
}