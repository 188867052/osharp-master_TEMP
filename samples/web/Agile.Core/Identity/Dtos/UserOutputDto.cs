using System;
using Liuliu.Demo.Identity.Entities;
using OSharp.Entity;
using OSharp.Mapping;

namespace Agile.Core.Identity.Dtos
{
    /// <summary>
    /// 输出DTO:用户信息
    /// </summary>
    [MapFrom(typeof(User))]
    public class UserOutputDto : IOutputDto, IDataAuthEnabled
    {
        /// <summary>
        /// 初始化一个<see cref="UserOutputDto"/>类型的新实例
        /// </summary>
        public UserOutputDto(User u)
        {
            this.Id = u.Id;
            this.UserName = u.UserName;
            this.NickName = u.NickName;
            this.Email = u.Email;
            this.EmailConfirmed = u.EmailConfirmed;
            this.PhoneNumber = u.PhoneNumber;
            this.PhoneNumberConfirmed = u.PhoneNumberConfirmed;
            this.LockoutEnd = u.LockoutEnd;
            this.LockoutEnabled = u.LockoutEnabled;
            this.AccessFailedCount = u.AccessFailedCount;
            this.IsLocked = u.IsLocked;
            this.CreatedTime = u.CreatedTime;
        }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 表示用户是否已确认其电子邮件地址的标志
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否已确认
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 当任何用户锁定结束时，UTC的日期和时间。
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 获取或设置 指示用户是否可以被锁定的标志。
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 获取或设置 当前用户失败的登录尝试次数。
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 角色信息集合
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 获取或设置 是否可更新
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// 获取或设置 是否可删除
        /// </summary>
        public bool Deletable { get; set; }
    }
}