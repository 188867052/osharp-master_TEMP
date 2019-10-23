using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Module
    {
        public Module()
        {
            this.InverseParent = new HashSet<Module>();
            this.ModuleFunction = new HashSet<ModuleFunction>();
            this.ModuleRole = new HashSet<ModuleRole>();
            this.ModuleUser = new HashSet<ModuleUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public string Code { get; set; }

        public double OrderCode { get; set; }

        public string TreePathString { get; set; }

        public int? ParentId { get; set; }

        public virtual Module Parent { get; set; }

        public virtual ICollection<Module> InverseParent { get; set; }

        public virtual ICollection<ModuleFunction> ModuleFunction { get; set; }

        public virtual ICollection<ModuleRole> ModuleRole { get; set; }

        public virtual ICollection<ModuleUser> ModuleUser { get; set; }
    }
}
