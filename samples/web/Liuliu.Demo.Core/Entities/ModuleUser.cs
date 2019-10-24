using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 用户模块信息.
    /// </summary>
    public partial class ModuleUser
    {
        public Guid Id { get; set; }

        public int ModuleId { get; set; }

        public int UserId { get; set; }

        public bool Disabled { get; set; }

        public virtual Module Module { get; set; }

        public virtual User User { get; set; }
    }
}
