using System;
using System.ComponentModel;
using OSharp.Entity;
using OSharp.Identity;

namespace Liuliu.Demo.Core.Release.Entities
{
    /// <summary>
    /// 用户信息基类
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public abstract class VersionsBase<TUserKey> : EntityBase<TUserKey>, ICreatedTime, ILockable, ISoftDeletable
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 初始化一个<see cref="UserBase{TUserKey}"/>类型的新实例
        /// </summary>
        protected VersionsBase()
        {
            this.CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 数据逻辑删除时间，为null表示正常数据，有值表示已逻辑删除，同时删除时间每次不同也能保证索引唯一性
        /// </summary>
        public DateTime? DeletedTime { get; set; }
    }
}