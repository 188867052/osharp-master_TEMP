using System;
using Liuliu.Demo.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSharp.Entity;

namespace Agile.Core.EntityConfiguration.Identity
{
    public class UserLoginConfiguration : EntityTypeConfigurationBase<UserLogin, Guid>
    {
        /// <summary>
        /// 重写以实现实体类型各个属性的数据库配置
        /// </summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasIndex(m => new { m.LoginProvider, m.ProviderKey }).HasName("UserLoginIndex").IsUnique();
            builder.HasOne(ul => ul.User).WithMany(u => u.UserLogins).HasForeignKey(ul => ul.UserId).IsRequired();
        }
    }
}