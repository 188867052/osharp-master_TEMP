﻿using System;
using System.ComponentModel;
using System.Linq;
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
    [ModuleInfo(Order = 3, Position = "Systems", PositionName = "系统管理模块")]
    [Description("管理-数据审计信息")]
    public class AuditEntityController : AdminApiController
    {
        private readonly IAuditContract _auditContract;
        private readonly IFilterService _filterService;

        /// <summary>
        /// 初始化一个<see cref="AuditEntityController"/>类型的新实例
        /// </summary>
        public AuditEntityController(IAuditContract auditContract, IFilterService filterService)
        {
            this._auditContract = auditContract;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取数据审计信息列表
        /// </summary>
        /// <param name="request">页请求</param>
        /// <returns></returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<AuditEntityOutputDto> Read(PageRequest request)
        {
            Expression<Func<AuditEntity, bool>> predicate = this._filterService.GetExpression<AuditEntity>(request.FilterGroup);
            PageResult<AuditEntityOutputDto> page;

            // 有操作参数，是从操作列表来的
            if (request.FilterGroup.Rules.Any(m => m.Field == "OperationId"))
            {
                page = this._auditContract.AuditEntities.ToPage(predicate, request.PageCondition, m => new AuditEntityOutputDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    TypeName = m.TypeName,
                    EntityKey = m.EntityKey,
                    OperateType = m.OperateType,
                    Properties = this._auditContract.AuditProperties.Where(n => n.AuditEntityId == m.Id).OrderBy(n => n.FieldName != "Id").ThenBy(n => n.FieldName).Select(n => new AuditPropertyOutputDto()
                    {
                        DisplayName = n.DisplayName,
                        FieldName = n.FieldName,
                        OriginalValue = n.OriginalValue,
                        NewValue = n.NewValue,
                        DataType = n.DataType,
                    }).ToList(),
                });
                return page.ToPageData();
            }

            request.AddDefaultSortCondition(new SortCondition("Operation.CreatedTime", ListSortDirection.Descending));
            page = this._auditContract.AuditEntities.ToPage(predicate, request.PageCondition, m => new AuditEntityOutputDto
            {
                Id = m.Id,
                Name = m.Name,
                TypeName = m.TypeName,
                EntityKey = m.EntityKey,
                OperateType = m.OperateType,
                NickName = m.Operation.NickName,
                FunctionName = m.Operation.FunctionName,
                CreatedTime = m.Operation.CreatedTime,
                Properties = this._auditContract.AuditProperties.Where(n => n.AuditEntityId == m.Id).OrderBy(n => n.FieldName != "Id").ThenBy(n => n.FieldName).Select(n => new AuditPropertyOutputDto()
                {
                    DisplayName = n.DisplayName,
                    FieldName = n.FieldName,
                    OriginalValue = n.OriginalValue,
                    NewValue = n.NewValue,
                    DataType = n.DataType,
                }).ToList(),
            });
            return page.ToPageData();
        }
    }
}