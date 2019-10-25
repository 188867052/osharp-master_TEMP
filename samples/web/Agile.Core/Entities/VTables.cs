using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class VTables
    {
        public string Name { get; set; }

        public int? Rows { get; set; }

        public short? KeyCount { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间.
        /// </summary>
        public DateTime ModifyDate { get; set; }
    }
}
