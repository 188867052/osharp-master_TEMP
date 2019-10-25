using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc;
using OSharp.Caching;
using OSharp.Core.Functions;
using OSharp.Core.Modules;
using OSharp.Filter;
using Check = OSharp.Data.Check;

namespace Liuliu.Demo.Web.Areas.Admin.Controllers.Release
{
    [ModuleInfo(Order = 1, Position = "RouteAnalyzer", PositionName = "RouteAnalyzer模块")]
    [Description("管理-RouteAnalyzer")]
    public class RouteAnalyzerController : AdminApiController
    {
        private readonly IRouteAnalyzer routeAnalyzer;
        private readonly ICacheService _cacheService;
        private readonly IFilterService _filterService;

        public RouteAnalyzerController(IRouteAnalyzer routeAnalyzer, ICacheService cacheService, IFilterService filterService)
        {
            this.routeAnalyzer = routeAnalyzer;
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
        public PageData<RouteInfo> Read(PageRequest request)
        {
            Check.NotNull(request, nameof(request));

            IFunction function = this.GetExecuteFunction();
            var predicate = this._filterService.GetExpression<RouteInfo>(request.FilterGroup);
            var source = this.routeAnalyzer.GetAllRouteInfo().AsQueryable();
            var page = this._cacheService.ToPageCache(source, predicate, request.PageCondition, m => m, function)
                              .ToPageResult(data => data.Select(m => m).ToArray());

            return page.ToPageData();
        }
    }
}