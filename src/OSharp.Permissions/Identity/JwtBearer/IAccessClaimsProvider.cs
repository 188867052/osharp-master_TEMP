﻿// -----------------------------------------------------------------------
//  <copyright file="IAccessClaimsProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2019 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2019-06-15 12:11</last-date>
// -----------------------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;

namespace OSharp.Identity.JwtBearer
{
    /// <summary>
    /// 定义AccessToken的用户Claims提供器
    /// </summary>
    public interface IAccessClaimsProvider
    {
        /// <summary>
        /// 创建用户标识
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Claim[]> CreateClaims(string userId);
    }
}