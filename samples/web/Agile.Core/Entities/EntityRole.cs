using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 数据角色信息.
    /// </summary>
    public partial class EntityRole
    {
        public Guid Id { get; set; }

        public int RoleId { get; set; }

        public Guid EntityId { get; set; }

        public int Operation { get; set; }

        public string FilterGroupJson { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual EntityInfo Entity { get; set; }

        public virtual Role Role { get; set; }
    }
}
