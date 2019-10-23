using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSharp.Core.Packs;
using OSharp.Exceptions;
using OSharp.Extensions;

namespace OSharp.Redis
{
    /// <summary>
    /// Redis模块基类
    /// </summary>
    public abstract class RedisPackBase : OsharpPack
    {
        private bool _enabled = false;

        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level => PackLevel.Framework;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();
            this._enabled = configuration["OSharp:Redis:Enabled"].CastTo(false);
            if (!this._enabled)
            {
                return services;
            }

            string config = configuration["OSharp:Redis:Configuration"];
            if (config.IsNullOrEmpty())
            {
                throw new OsharpException("配置文件中Redis节点的Configuration不能为空");
            }

            string name = configuration["OSharp:Redis:InstanceName"].CastTo("RedisName");

            services.RemoveAll(typeof(IDistributedCache));
            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = config;
                opts.InstanceName = name;
            });

            return services;
        }

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void UsePack(IServiceProvider provider)
        {
            this.IsEnabled = this._enabled;
        }
    }
}