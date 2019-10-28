using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Agile.Core.Identity.Dtos;
using Agile.Core.Identity.Entities;
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
    [ModuleInfo(Order = 3, Position = "Security", PositionName = "权限安全模块")]
    [Description("管理-角色功能")]
    public class RoleFunctionController : AdminApiController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IFilterService _filterService;
        private readonly SecurityManager _securityManager;

        public RoleFunctionController(
            SecurityManager securityManager,
            RoleManager<Role> roleManager,
            IFilterService filterService)
        {
            this._securityManager = securityManager;
            this._roleManager = roleManager;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取角色信息
        /// </summary>
        /// <returns>角色信息</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<RoleOutputDto2> Read(PageRequest request)
        {
            request.FilterGroup.Rules.Add(new FilterRule("IsLocked", false, FilterOperate.Equal));
            Expression<Func<Role, bool>> predicate = this._filterService.GetExpression<Role>(request.FilterGroup);
            PageResult<RoleOutputDto2> page = this._roleManager.Roles.ToPage<Role, RoleOutputDto2>(predicate, request.PageCondition);
            return page.ToPageData();
        }

        /// <summary>
        /// 读取角色功能信息
        /// </summary>
        /// <returns>角色功能信息</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [Description("读取功能")]
        public PageData<FunctionOutputDto2> ReadFunctions(int roleId, [FromBody]PageRequest request)
        {
            if (roleId == 0)
            {
                return new PageData<FunctionOutputDto2>();
            }

            int[] moduleIds = this._securityManager.GetRoleModuleIds(roleId);
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

            var page = this._securityManager.Functions.ToPage<Function, FunctionOutputDto2>(funcExp, request.PageCondition);
            return page.ToPageData();
        }
    }
}