﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Liuliu.Demo.Systems.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OSharp.AspNetCore.Mvc;
using OSharp.Caching;
using OSharp.Core.Functions;
using OSharp.Core.Modules;
using OSharp.Core.Packs;
using OSharp.Filter;
using OSharp.Reflection;

namespace Agile.Web.Areas.Admin.Controllers.Systems
{
    [ModuleInfo(Order = 4, Position = "Systems", PositionName = "系统管理模块")]
    [Description("管理-模块包信息")]
    public class PackController : AdminApiController
    {
        private readonly ICacheService _cacheService;
        private readonly IFilterService _filterService;

        /// <summary>
        /// 初始化一个<see cref="PackController"/>类型的新实例
        /// </summary>
        public PackController(
            ICacheService cacheService,
            IFilterService filterService)
        {
            this._cacheService = cacheService;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取模块包列表信息
        /// </summary>
        /// <param name="request">页请求</param>
        /// <returns></returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取模块包")]
        public PageData<PackOutputDto> Read(PageRequest request)
        {
            request.AddDefaultSortCondition(
                new SortCondition("Level"),
                new SortCondition("Order")
            );
            IFunction function = this.GetExecuteFunction();
            Expression<Func<OsharpPack, bool>> exp = this._filterService.GetExpression<OsharpPack>(request.FilterGroup);
            IServiceProvider provider = this.HttpContext.RequestServices;
            IOsharpPackManager manager = provider.GetService<IOsharpPackManager>();
            return this._cacheService.ToPageCache(manager.SourcePacks.AsQueryable(), exp,
                request.PageCondition,
                m => new PackOutputDto()
                {
                    Name = m.GetType().Name,
                    Display = m.GetType().GetDescription(false),
                    Class = m.GetType().FullName,
                    Level = m.Level,
                    Order = m.Order,
                    IsEnabled = m.IsEnabled,
                },
                function).ToPageData();
        }
    }
}