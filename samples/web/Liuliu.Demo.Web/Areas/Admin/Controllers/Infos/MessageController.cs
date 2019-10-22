// -----------------------------------------------------------------------
// <once-generated>
//     这个文件只生成一次，再次生成不会被覆盖。
//     可以在此类进行继承重写来扩展基类 MessageControllerBase
// </once-generated>
//
//  <copyright file="Message.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2019 OSharp. All rights reserved.
//  </copyright>
//  <site>https://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
// -----------------------------------------------------------------------

using System;

using OSharp.Filter;

using Liuliu.Demo.Infos;
using OSharp.Core.Modules;
using System.ComponentModel;

namespace Liuliu.Demo.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理控制器: 站内信信息
    /// </summary>
    public class MessageController : MessageControllerBase
    {
        /// <summary>
        /// 初始化一个<see cref="MessageController"/>类型的新实例
        /// </summary>
        public MessageController(IInfosContract infosContract,
            IFilterService filterService)
            : base(infosContract, filterService)
        { }
    }

    [ModuleInfo(Position = "Infos2", PositionName = "信息模块2")]
    [Description("管理-站内信信息2")]
    public class Message2Controller : MessageControllerBase
    {
        /// <summary>
        /// 初始化一个<see cref="MessageController"/>类型的新实例
        /// </summary>
        public Message2Controller(IInfosContract infosContract,
            IFilterService filterService)
            : base(infosContract, filterService)
        { }
    }
}
