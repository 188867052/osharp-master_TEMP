using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class UserToken
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public string LoginProvider { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public virtual User User { get; set; }
    }
}
