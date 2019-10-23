using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSharp.Core.Packs;

namespace OSharp.CodeGeneration
{
    /// <summary>
    /// 代码生成模块
    /// </summary>
    [Description("代码生成模块")]
    public class CodeGenerationPack : OsharpPack
    {
        /// <summary>
        /// 获取 模块级别，级别越小越先启动
        /// </summary>
        public override PackLevel Level { get; } = PackLevel.Application;

        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.TryAddSingleton<ICodeGenerator, RazorCodeGenerator>();

            return services;
        }
    }
}