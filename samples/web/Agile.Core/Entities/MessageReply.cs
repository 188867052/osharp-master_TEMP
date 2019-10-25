using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class MessageReply
    {
        public MessageReply()
        {
            this.InverseParentReply = new HashSet<MessageReply>();
        }

        public Guid Id { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public Guid ParentMessageId { get; set; }

        public Guid ParentReplyId { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? DeletedTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int UserId { get; set; }

        public Guid BelongMessageId { get; set; }

        public virtual Message BelongMessage { get; set; }

        public virtual Message ParentMessage { get; set; }

        public virtual MessageReply ParentReply { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<MessageReply> InverseParentReply { get; set; }
    }
}
