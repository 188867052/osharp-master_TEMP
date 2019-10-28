using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Agile.Core.Identity;
using Agile.Core.Identity.Dtos;
using Agile.Core.Identity.Entities;
using Microsoft.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc.Filters;
using OSharp.AspNetCore.UI;
using OSharp.Core.Modules;
using OSharp.Data;
using OSharp.Entity;
using OSharp.Filter;
using OSharp.Security;

namespace Agile.Web.Areas.Admin.Controllers.Identity
{
    [ModuleInfo(Order = 3, Position = "Identity", PositionName = "身份认证模块")]
    [Description("管理-用户角色信息")]
    public class UserRoleController : AdminApiController
    {
        private readonly IIdentityContract _identityContract;
        private readonly IFilterService _filterService;

        public UserRoleController(
            IIdentityContract identityContract,
            IFilterService filterService)
        {
            this._identityContract = identityContract;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取用户角色信息
        /// </summary>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<UserRoleOutputDto> Read(PageRequest request)
        {
            Expression<Func<UserRole, bool>> predicate = this._filterService.GetExpression<UserRole>(request.FilterGroup);
            Func<UserRole, bool> updateFunc = this._filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Update).Compile();
            Func<UserRole, bool> deleteFunc = this._filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Delete).Compile();

            PageResult<UserRoleOutputDto> page = this._identityContract.UserRoles.ToPage(predicate, request.PageCondition, m => new
            {
                D = m,
                m.User.UserName,
                RoleName = m.Role.Name,
            }).ToPageResult(data => data.Select(m => new UserRoleOutputDto(m.D)
            {
                UserName = m.UserName,
                RoleName = m.RoleName,
                Updatable = updateFunc(m.D),
                Deletable = deleteFunc(m.D),
            }).ToArray());
            return page.ToPageData();
        }

        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="dtos">用户角色信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("更新")]
        public async Task<AjaxResult> Update(UserRoleInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));

            OperationResult result = await this._identityContract.UpdateUserRoles(dtos);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ids">要删除的用户角色编号</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("删除")]
        public async Task<AjaxResult> Delete(Guid[] ids)
        {
            Check.NotNull(ids, nameof(ids));

            OperationResult result = await this._identityContract.DeleteUserRoles(ids);
            return result.ToAjaxResult();
        }
    }
}