using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class ModuleFunction
    {
        public Guid Id { get; set; }

        public int ModuleId { get; set; }

        public Guid FunctionId { get; set; }

        public virtual Function Function { get; set; }

        public virtual Module Module { get; set; }
    }
}
