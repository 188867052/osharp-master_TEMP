using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class AuditEntity
    {
        public AuditEntity()
        {
            this.AuditProperty = new HashSet<AuditProperty>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public string EntityKey { get; set; }

        public int OperateType { get; set; }

        public Guid OperationId { get; set; }

        public virtual AuditOperation Operation { get; set; }

        public virtual ICollection<AuditProperty> AuditProperty { get; set; }
    }
}
