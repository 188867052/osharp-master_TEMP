using System;
using Liuliu.Demo.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSharp.Entity;

namespace Liuliu.Demo.EntityConfiguration.Security
{
    public class EntityUserConfiguration : EntityTypeConfigurationBase<EntityUser, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<EntityUser> builder)
        {
            builder.HasIndex(m => new { m.EntityId, m.UserId }).HasName("EntityUserIndex");

            builder.HasOne(eu => eu.EntityInfo).WithMany().HasForeignKey(m => m.EntityId);
            builder.HasOne(eu => eu.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}