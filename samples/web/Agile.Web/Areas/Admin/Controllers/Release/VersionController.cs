using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Agile.Core.Common.Dtos;
using Agile.Core.Identity;
using Liuliu.Demo.Core.Release.Entities;
using Liuliu.Demo.Identity.Entities;
using Liuliu.Demo.Security;
using Liuliu.Demo.Security.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc.Filters;
using OSharp.AspNetCore.UI;
using OSharp.Caching;
using OSharp.Collections;
using OSharp.Core.Functions;
using OSharp.Core.Modules;
using OSharp.Data;
using OSharp.Entity;
using OSharp.Filter;
using OSharp.Mapping;
using OSharp.Security;
using VersionInputDto = Liuliu.Demo.Core.Release.Dtos.VersionInputDto;
using VersionOutputDto = Liuliu.Demo.Identity.Dtos.VersionOutputDto;

namespace Liuliu.Demo.Web.Areas.Admin.Controllers.Release
{
    [ModuleInfo(Order = 1, Position = "Release", PositionName = "版本管理模块")]
    [Description("管理-版本管理")]
    public class VersionController : AdminApiController
    {
        private readonly IIdentityContract _identityContract;
        private readonly ICacheService _cacheService;
        private readonly IRepository<Versions, int> versionRepository;
        private readonly IFilterService _filterService;
        private readonly SecurityManager _securityManager;
        private readonly UserManager<User> _userManager;

        public VersionController(
            UserManager<User> userManager,
            SecurityManager securityManager,
            IIdentityContract identityContract,
            ICacheService cacheService,
            IRepository<Versions, int> versionRepository,
            IFilterService filterService)
        {
            this._userManager = userManager;
            this._securityManager = securityManager;
            this._identityContract = identityContract;
            this._cacheService = cacheService;
            this.versionRepository = versionRepository;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取版本列表信息
        /// </summary>
        /// <returns>版本列表信息</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<VersionOutputDto> Read(PageRequest request)
        {
            Check.NotNull(request, nameof(request));
            IFunction function = this.GetExecuteFunction();

            Func<Versions, bool> updateFunc = this._filterService.GetDataFilterExpression<Versions>(null, DataAuthOperation.Update).Compile();
            Func<Versions, bool> deleteFunc = this._filterService.GetDataFilterExpression<Versions>(null, DataAuthOperation.Delete).Compile();
            Expression<Func<Versions, bool>> predicate = this._filterService.GetExpression<Versions>(request.FilterGroup);
            var source = ((Repository<Versions, int>)this.versionRepository).Entities;
            var page = this._cacheService.ToPageCache(source, predicate, request.PageCondition, m => new
            {
                D = m,
            }, function)
                .ToPageResult(data => data.Select(m => new VersionOutputDto(m.D)
                {
                    Updatable = updateFunc(m.D),
                    Deletable = deleteFunc(m.D),
                }).ToArray());
            return page.ToPageData();
        }

        /// <summary>
        /// 读取版本节点信息
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("读取节点")]
        public ListNode[] ReadNode(FilterGroup group)
        {
            Check.NotNull(group, nameof(group));
            IFunction function = this.GetExecuteFunction();
            Expression<Func<User, bool>> exp = this._filterService.GetExpression<User>(group);
            Expression<Func<User, ListNode>> selector = m => new ListNode()
            {
                Id = m.Id,
                Name = m.NickName,
            };
            ListNode[] nodes = this._cacheService.ToCacheArray(this._userManager.Users, exp, selector, function);
            return nodes;
        }

        /// <summary>
        /// 新增版本信息
        /// </summary>
        /// <param name="dtos">版本信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("新增")]
        public async Task<AjaxResult> Create(VersionInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));
            List<string> names = new List<string>();
            foreach (var dto in dtos)
            {
                Versions version = dto.MapTo<Versions>();
                var count = await this.versionRepository.InsertAsync(version);
                if (count == 0)
                {
                    return new AjaxResult($"版本“{version.Name}”创建失败");
                }

                names.Add(version.Name);
            }

            return new AjaxResult($"版本“{names.ExpandAndToString()}”创建成功");
        }

        /// <summary>
        /// 更新版本信息
        /// </summary>
        /// <param name="dtos">版本信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("更新")]
        public async Task<AjaxResult> Update(VersionInputDto[] dtos)
        {
            Check.NotNull(dtos, nameof(dtos));
            List<string> names = new List<string>();
            foreach (var dto in dtos)
            {
                var version = await this.versionRepository.GetAsync(dto.Id);
                var newVersion = dto.MapTo(version);
                var count = await this.versionRepository.UpdateAsync(newVersion);
                if (count == 0)
                {
                    return new AjaxResult($"版本“{version.Name}”更新失败");
                }

                names.Add(version.Name);
            }

            return new AjaxResult($"版本“{names.ExpandAndToString()}”更新成功");
        }

        /// <summary>
        /// 删除版本信息
        /// </summary>
        /// <param name="ids">版本信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [UnitOfWork]
        [Description("删除")]
        public async Task<AjaxResult> Delete(int[] ids)
        {
            Check.NotNull(ids, nameof(ids));
            List<string> names = new List<string>();
            foreach (int id in ids)
            {
                var version = await this.versionRepository.GetAsync(id);
                var count = await this.versionRepository.DeleteAsync(id);
                if (count == 0)
                {
                    return new AjaxResult($"版本“{names.ExpandAndToString()}”删除失败");
                }

                names.Add(version.Name);
            }

            return new AjaxResult($"版本“{names.ExpandAndToString()}”删除成功");
        }

        /// <summary>
        /// 设置版本角色
        /// </summary>
        /// <param name="dto">版本角色信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [DependOnFunction("ReadUserRoles", Controller = "Role")]
        [UnitOfWork]
        [Description("设置角色")]
        public async Task<AjaxResult> SetRoles(UserSetRoleDto dto)
        {
            OperationResult result = await this._identityContract.SetUserRoles(dto.UserId, dto.RoleIds);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 设置版本模块
        /// </summary>
        /// <param name="dto">版本模块信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("Read")]
        [DependOnFunction("ReadUserModules", Controller = "Module")]
        [UnitOfWork]
        [Description("设置模块")]
        public async Task<AjaxResult> SetModules(UserSetModuleDto dto)
        {
            OperationResult result = await this._securityManager.SetUserModules(dto.UserId, dto.ModuleIds);
            return result.ToAjaxResult();
        }
    }
}