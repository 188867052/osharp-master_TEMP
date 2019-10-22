// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using OSharp.Entity;
using OSharp.Identity;
using System;
using System.ComponentModel;


namespace Liuliu.Demo.Identity.Entities
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
            CreatedTime = DateTime.Now;
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