// -----------------------------------------------------------------------
//  <copyright file="AuthorizationResult.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-05-10 19:45</last-date>
// -----------------------------------------------------------------------

using System.Diagnostics;

using OSharp.Data;
using OSharp.Extensions;

namespace OSharp.Security
{
    /// <summary>
    /// 权限检查结果
    /// </summary>
    [DebuggerDisplay("{ResultType}-{Message}")]
    public sealed class AuthorizationResult : OsharpResult<AuthorizationStatus>
    {
        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status)
            : this(status, null)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status, string message)
            : this(status, message, null)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AuthorizationResult"/>类型的新实例
        /// </summary>
        public AuthorizationResult(AuthorizationStatus status, string message, object data)
            : base(status, message, data)
        {
        }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public override string Message
        {
            get { return this._message ?? this.ResultType.ToDescription(); }
            set { this._message = value; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsOk
        {
            get { return this.ResultType == AuthorizationStatus.OK; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsUnauthorized
        {
            get { return this.ResultType == AuthorizationStatus.Unauthorized; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsForbidden
        {
            get { return this.ResultType == AuthorizationStatus.Forbidden; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsNoFound
        {
            get { return this.ResultType == AuthorizationStatus.NoFound; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsLocked
        {
            get { return this.ResultType == AuthorizationStatus.Locked; }
        }

        /// <summary>
        /// 获取 是否 <see cref="AuthorizationStatus.OK"/>
        /// </summary>
        public bool IsError
        {
            get { return this.ResultType == AuthorizationStatus.Error; }
        }

        /// <summary>
        /// 获取 检查成功的结果
        /// </summary>
        public static AuthorizationResult OK { get; } = new AuthorizationResult(AuthorizationStatus.OK);
    }
}