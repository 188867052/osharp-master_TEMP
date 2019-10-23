using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Message
    {
        public Message()
        {
            this.MessageReceive = new HashSet<MessageReceive>();
            this.MessageReplyBelongMessage = new HashSet<MessageReply>();
            this.MessageReplyParentMessage = new HashSet<MessageReply>();
            this.Role = new HashSet<Role>();
            this.User = new HashSet<User>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int MessageType { get; set; }

        public int NewReplyCount { get; set; }

        public bool IsSended { get; set; }

        public bool CanReply { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? DeletedTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public int SenderId { get; set; }

        public virtual User Sender { get; set; }

        public virtual ICollection<MessageReceive> MessageReceive { get; set; }

        public virtual ICollection<MessageReply> MessageReplyBelongMessage { get; set; }

        public virtual ICollection<MessageReply> MessageReplyParentMessage { get; set; }

        public virtual ICollection<Role> Role { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
