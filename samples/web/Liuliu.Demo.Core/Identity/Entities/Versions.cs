// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel;


namespace Liuliu.Demo.Identity.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Description("版本信息")]
    public class Versions : VersionsBase<int>
    {
        /// <summary>
        /// 版本名称.
        /// </summary>
        public string Name { get; set; }
    }
}