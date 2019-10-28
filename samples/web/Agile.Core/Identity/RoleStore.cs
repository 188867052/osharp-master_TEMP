using Agile.Core.Identity.Entities;
using OSharp.Entity;
using OSharp.Identity;

namespace Agile.Core.Identity
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleStore : RoleStoreBase<Role, int, RoleClaim>
    {
        /// <summary>
        /// 初始化一个<see cref="RoleStoreBase{TRole,TRoleKey,TRoleClaim}"/>类型的新实例
        /// </summary>
        public RoleStore(IRepository<Role, int> roleRepository, IRepository<RoleClaim, int> roleClaimRepository)
            : base(roleRepository, roleClaimRepository)
        {
        }
    }
}