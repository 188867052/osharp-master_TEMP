using System;
using System.Collections.Generic;

namespace Entities
{
    /// <summary>
    /// 用户表.
    /// </summary>
    public partial class User
    {
        public User()
        {
            this.EntityUser = new HashSet<EntityUser>();
            this.LoginLog = new HashSet<LoginLog>();
            this.Message = new HashSet<Message>();
            this.MessageReceive = new HashSet<MessageReceive>();
            this.MessageReply = new HashSet<MessageReply>();
            this.ModuleUser = new HashSet<ModuleUser>();
            this.UserClaim = new HashSet<UserClaim>();
            this.UserLogin = new HashSet<UserLogin>();
            this.UserRole = new HashSet<UserRole>();
            this.UserToken = new HashSet<UserToken>();
        }

        /// <summary>
        /// 主键.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 标准化的用户名.
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// NickName.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 电子邮箱.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 标准化的电子邮箱.
        /// </summary>
        public string NormalizeEmail { get; set; }

        /// <summary>
        /// 电子邮箱确认.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 密码哈希值.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 用户头像.
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 安全标识.
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 版本标识.
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 手机号码.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号码确定.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 双因子身份验证.
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 锁定时间.
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 是否登录锁.
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 登录失败次数.
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 是否系统用户.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 是否锁定.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 数据逻辑删除时间.
        /// </summary>
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// 备注.
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public Guid? MessageId { get; set; }

        public virtual Message MessageNavigation { get; set; }

        public virtual UserDetail UserDetail { get; set; }

        public virtual ICollection<EntityUser> EntityUser { get; set; }

        public virtual ICollection<LoginLog> LoginLog { get; set; }

        public virtual ICollection<Message> Message { get; set; }

        public virtual ICollection<MessageReceive> MessageReceive { get; set; }

        public virtual ICollection<MessageReply> MessageReply { get; set; }

        public virtual ICollection<ModuleUser> ModuleUser { get; set; }

        public virtual ICollection<UserClaim> UserClaim { get; set; }

        public virtual ICollection<UserLogin> UserLogin { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

        public virtual ICollection<UserToken> UserToken { get; set; }
    }
}
