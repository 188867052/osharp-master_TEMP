using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 数据用户信息.
    /// </summary>
    public partial class EntityUser
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public Guid EntityId { get; set; }

        public string FilterGroupJson { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual EntityInfo Entity { get; set; }

        public virtual User User { get; set; }
    }
}
