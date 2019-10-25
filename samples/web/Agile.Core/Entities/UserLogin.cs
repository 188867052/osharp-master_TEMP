using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class UserLogin
    {
        public Guid Id { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedTime { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
