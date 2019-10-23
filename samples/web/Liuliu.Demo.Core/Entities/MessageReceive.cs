using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class MessageReceive
    {
        public Guid Id { get; set; }

        public DateTime ReadDate { get; set; }

        public int NewReplyCount { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid MessageId { get; set; }

        public int UserId { get; set; }

        public virtual Message Message { get; set; }

        public virtual User User { get; set; }
    }
}
