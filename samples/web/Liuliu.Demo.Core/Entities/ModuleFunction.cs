using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 模块功能信息.
    /// </summary>
    public partial class ModuleFunction
    {
        public Guid Id { get; set; }

        public int ModuleId { get; set; }

        public Guid FunctionId { get; set; }

        public virtual Function Function { get; set; }

        public virtual Module Module { get; set; }
    }
}
