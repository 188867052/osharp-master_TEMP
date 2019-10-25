using System.Collections.Generic;

namespace Liuliu.Demo.Web
{
    public interface IRouteAnalyzer
    {
        IList<RouteInfo> GetAllRouteInfo();
    }
}