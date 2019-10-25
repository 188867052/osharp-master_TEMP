using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Versions
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime? DeletedTime { get; set; }

        public bool IsLocked { get; set; }
    }
}
