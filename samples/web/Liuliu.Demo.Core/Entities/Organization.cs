using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Organization
    {
        public Organization()
        {
            this.InverseParent = new HashSet<Organization>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public int? ParentId { get; set; }

        public virtual Organization Parent { get; set; }

        public virtual ICollection<Organization> InverseParent { get; set; }
    }
}
