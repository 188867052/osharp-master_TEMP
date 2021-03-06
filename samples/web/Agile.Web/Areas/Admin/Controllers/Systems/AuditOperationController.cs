﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Liuliu.Demo.Systems;
using Liuliu.Demo.Systems.Dtos;
using Liuliu.Demo.Systems.Entities;
using Microsoft.AspNetCore.Mvc;
using OSharp.Core.Modules;
using OSharp.Entity;
using OSharp.Filter;

namespace Agile.Web.Areas.Admin.Controllers.Systems
{
    [ModuleInfo(Order = 2, Position = "Systems", PositionName = "系统管理模块")]
    [Description("管理-操作审计信息")]
    public class AuditOperationController : AdminApiController
    {
        private readonly IAuditContract _auditContract;
        private readonly IFilterService _filterService;

        public AuditOperationController(IAuditContract auditContract, IFilterService filterService)
        {
            this._auditContract = auditContract;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取操作审计信息
        /// </summary>
        /// <param name="request">页数据请求</param>
        /// <returns>操作审计信息的页数据</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<AuditOperationOutputDto> Read(PageRequest request)
        {
            Expression<Func<AuditOperation, bool>> predicate = this._filterService.GetExpression<AuditOperation>(request.FilterGroup);
            request.AddDefaultSortCondition(new SortCondition("CreatedTime", ListSortDirection.Descending));
            var page = this._auditContract.AuditOperations.ToPage<AuditOperation, AuditOperationOutputDto>(predicate, request.PageCondition);
            return page.ToPageData();
        }
    }
}