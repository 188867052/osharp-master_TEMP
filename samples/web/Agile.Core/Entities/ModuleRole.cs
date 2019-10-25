using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 角色模块信息.
    /// </summary>
    public partial class ModuleRole
    {
        public Guid Id { get; set; }

        public int ModuleId { get; set; }

        public int RoleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual Role Role { get; set; }
    }
}
