namespace Agile.Web.Startups
{
#if NETCOREAPP2_2
    /// <summary>
    /// SignalR模块
    /// </summary>
    [Description("SignalR模块")]
    public class SignalRPack : SignalRPackBase
    {
        /// <summary>
        /// 重写以获取Hub路由创建委托
        /// </summary>
        /// <param name="serviceProvider">服务提供者</param>
        /// <returns></returns>
        protected override Action<HubRouteBuilder> GetHubRouteBuildAction(IServiceProvider serviceProvider)
        {
            return new Action<HubRouteBuilder>(builder =>
            {
                // 在这实现Hub的路由映射
                // 例如：builder.MapHub<MyHub>();
            });
        } 
    }
#endif
}