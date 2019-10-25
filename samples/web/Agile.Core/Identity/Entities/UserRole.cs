using System.ComponentModel;
using Liuliu.Demo.Identity.Entities;
using OSharp.Identity;

namespace Agile.Core.Identity.Entities
{
    /// <summary>
    /// 实体类：用户角色信息
    /// </summary>
    [Description("用户角色信息")]
    public class UserRole : UserRoleBase<int, int>
    {
        /// <summary>
        /// 获取或设置 关联用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 获取或设置 关联角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}