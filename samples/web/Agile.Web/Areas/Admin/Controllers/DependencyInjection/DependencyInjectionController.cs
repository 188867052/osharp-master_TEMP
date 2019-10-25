using System.ComponentModel;
using System.Linq;
using DependencyInjection.Analyzer;
using Liuliu.Demo.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc;
using OSharp.Caching;
using OSharp.Core.Functions;
using OSharp.Core.Modules;
using OSharp.Filter;
using Check = OSharp.Data.Check;

namespace Agile.Web.Areas.Admin.Controllers.DependencyInjection
{
    [ModuleInfo(Order = 1, Position = "DependencyInjection", PositionName = "DependencyInjection模块")]
    [Description("管理-DependencyInjection")]
    public class DependencyInjectionController : AdminApiController
    {
        private readonly IDependencyInjectionAnalyzer serviceAnalyzer;
        private readonly ICacheService _cacheService;
        private readonly IFilterService _filterService;

        public DependencyInjectionController(IDependencyInjectionAnalyzer serviceAnalyzer, ICacheService cacheService, IFilterService filterService)
        {
            this.serviceAnalyzer = serviceAnalyzer;
            this._cacheService = cacheService;
            this._filterService = filterService;
        }

        /// <summary>
        /// 读取版本列表信息
        /// </summary>
        /// <returns>版本列表信息</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("读取")]
        public PageData<DependencyInjectionInfo> Read(PageRequest request)
        {
            Check.NotNull(request, nameof(request));

            IFunction function = this.GetExecuteFunction();
            var predicate = this._filterService.GetExpression<DependencyInjectionInfo>(request.FilterGroup);
            var source = this.serviceAnalyzer.GetDependencyInjectionInfo().AsQueryable();
            var page = this._cacheService.ToPageCache(source, predicate, request.PageCondition, m => m, function)
                              .ToPageResult(data => data.Select(m => m).ToArray());

            return page.ToPageData();
        }
    }
}