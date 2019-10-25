using Liuliu.Demo.Core.Release.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSharp.Entity;

namespace Liuliu.Demo.EntityConfiguration.Release
{
    public class VersionConfiguration : EntityTypeConfigurationBase<Versions, int>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<Versions> builder)
        {
            builder.HasIndex(m => new { m.Id }).HasName("VersionIdIndex").IsUnique();
        }
    }
}