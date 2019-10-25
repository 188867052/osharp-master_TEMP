using System.Collections.Generic;
using System.ComponentModel;
using OSharp.Identity;

namespace Agile.Core.Identity.Entities
{
    /// <summary>
    /// 实体类：角色信息
    /// </summary>
    [Description("角色信息")]
    public class Role : RoleBase<int>
    {
        /// <summary>
        /// 获取或设置 分配的用户角色信息集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 获取或设置 角色声明信息集合
        /// </summary>
        public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
    }
}