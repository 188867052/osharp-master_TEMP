using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class EntityInfo
    {
        public EntityInfo()
        {
            this.EntityRole = new HashSet<EntityRole>();
            this.EntityUser = new HashSet<EntityUser>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public bool AuditEnabled { get; set; }

        public string PropertyJson { get; set; }

        public virtual ICollection<EntityRole> EntityRole { get; set; }

        public virtual ICollection<EntityUser> EntityUser { get; set; }
    }
}
