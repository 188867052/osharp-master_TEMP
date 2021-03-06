﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Agile.Core.Identity.Dtos;
using Agile.Core.Identity.Entities;
using Liuliu.Demo.Identity.Entities;
using Liuliu.Demo.Security;
using Liuliu.Demo.Security.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OSharp.Core.Functions;
using OSharp.Core.Modules;
using OSharp.Entity;
using OSharp.Filter;
using OSharp.Linq;

namespace Agile.Web.Areas.Admin.Controllers.Security
{
    [ModuleInfo(Order = 4, Position = "Security", PositionName = "权限安全模块")]
    [Description("管理-用户功能")]
    public class UserFunctionController : AdminApiController
    {
        private readonly IFilterService _filterService;
        private readonly SecurityManager _securityManager;
        private readonly UserManager<User> _userManager;

        public UserFunctionController(
            SecurityManager securityManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IFilterService filterService)
        {
            this._securityManager = securityManager;
            this._userManager = userManager;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<UserOutputDto2> Read(PageRequest request)
        {
            request.FilterGroup.Rules.Add(new FilterRule("IsLocked", false, FilterOperate.Equal));
            Expression<Func<User, bool>> predicate = this._filterService.GetExpression<User>(request.FilterGroup);
            var page = this._userManager.Users.ToPage<User, UserOutputDto2>(predicate, request.PageCondition);
            return page.ToPageData();
        }

        /// <summary>
        /// 读取用户功能信息
        /// </summary>
        /// <returns>用户功能信息</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [Description("读取功能")]
        public PageData<FunctionOutputDto2> ReadFunctions(int userId, [FromBody]PageRequest request)
        {
            if (userId == 0)
            {
                return new PageData<FunctionOutputDto2>();
            }

            int[] moduleIds = this._securityManager.GetUserWithRoleModuleIds(userId);
            Guid[] functionIds = this._securityManager.ModuleFunctions.Where(m => moduleIds.Contains(m.ModuleId)).Select(m => m.FunctionId).Distinct()
                .ToArray();
            if (functionIds.Length == 0)
            {
                return new PageData<FunctionOutputDto2>();
            }

            Expression<Func<Function, bool>> funcExp = this._filterService.GetExpression<Function>(request.FilterGroup);
            funcExp = funcExp.And(m => functionIds.Contains(m.Id));
            if (request.PageCondition.SortConditions.Length == 0)
            {
                request.PageCondition.SortConditions = new[] { new SortCondition("Area"), new SortCondition("Controller") };
            }

            PageResult<FunctionOutputDto2> page = this._securityManager.Functions.ToPage<Function, FunctionOutputDto2>(funcExp, request.PageCondition);
            return page.ToPageData();
        }
    }
}