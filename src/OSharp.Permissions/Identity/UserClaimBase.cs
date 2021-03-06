﻿// -----------------------------------------------------------------------
//  <copyright file="UserClaimBase.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-09-04 21:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using OSharp.Entity;

namespace OSharp.Identity
{
    /// <summary>
    /// 用户声明基类
    /// </summary>
    /// <typeparam name="TUserKey">用户编号类型</typeparam>
    public abstract class UserClaimBase<TUserKey> : EntityBase<int>
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

        /// <summary>
        /// 获取或设置 声明类型
        /// </summary>
        [Required]
        [DisplayName("声明类型")]
        public string ClaimType { get; set; }

        /// <summary>
        /// 获取或设置 声明值
        /// </summary>
        [DisplayName("声明值")]
        public string ClaimValue { get; set; }

        /// <summary>
        /// 使用类型和值创建一个声明对象
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim()
        {
            return new Claim(this.ClaimType, this.ClaimValue);
        }

        /// <summary>
        /// 使用一个声明对象初始化
        /// </summary>
        /// <param name="other">声明对象</param>
        public virtual void InitializeFromClaim(Claim other)
        {
            this.ClaimType = other?.Type;
            this.ClaimValue = other?.Value;
        }
    }
}