using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using OSharp.Audits;
using OSharp.Core.Functions;

namespace OSharp.Dependency
{
    /// <summary>
    /// 基于Scoped生命周期的数据字典，可用于在Scoped环境中传递数据
    /// </summary>
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class ScopedDictionary : ConcurrentDictionary<string, object>, IDisposable
    {
        /// <summary>
        /// 获取或设置 当前执行的功能
        /// </summary>
        public IFunction Function { get; set; }

        /// <summary>
        /// 获取或设置 对于当前功能有效的角色集合，用于数据权限判断
        /// </summary>
        public string[] DataAuthValidRoleNames { get; set; } = new string[0];

        /// <summary>
        /// 获取或设置 当前操作审计
        /// </summary>
        public AuditOperationEntry AuditOperation { get; set; }

        /// <summary>
        /// 获取或设置 当前用户
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>释放资源.</summary>
        public void Dispose()
        {
            this.Function = null;
            this.AuditOperation = null;
            this.Identity = null;
            this.Clear();
        }
    }
}