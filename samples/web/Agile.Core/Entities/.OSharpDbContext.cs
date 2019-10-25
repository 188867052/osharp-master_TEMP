using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities
{
    public partial class OSharpDbContext : DbContext
    {
        public OSharpDbContext()
        {
        }

        public OSharpDbContext(DbContextOptions<OSharpDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditEntity> AuditEntity { get; set; }

        public virtual DbSet<AuditOperation> AuditOperation { get; set; }

        public virtual DbSet<AuditProperty> AuditProperty { get; set; }

        public virtual DbSet<EntityInfo> EntityInfo { get; set; }

        public virtual DbSet<EntityRole> EntityRole { get; set; }

        public virtual DbSet<EntityUser> EntityUser { get; set; }

        public virtual DbSet<Function> Function { get; set; }

        public virtual DbSet<KeyValue> KeyValue { get; set; }

        public virtual DbSet<LoginLog> LoginLog { get; set; }

        public virtual DbSet<Message> Message { get; set; }

        public virtual DbSet<MessageReceive> MessageReceive { get; set; }

        public virtual DbSet<MessageReply> MessageReply { get; set; }

        public virtual DbSet<Module> Module { get; set; }

        public virtual DbSet<ModuleFunction> ModuleFunction { get; set; }

        public virtual DbSet<ModuleRole> ModuleRole { get; set; }

        public virtual DbSet<ModuleUser> ModuleUser { get; set; }

        public virtual DbSet<Organization> Organization { get; set; }

        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<RoleClaim> RoleClaim { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<UserClaim> UserClaim { get; set; }

        public virtual DbSet<UserDetail> UserDetail { get; set; }

        public virtual DbSet<UserLogin> UserLogin { get; set; }

        public virtual DbSet<UserRole> UserRole { get; set; }

        public virtual DbSet<UserToken> UserToken { get; set; }

        public virtual DbSet<VColumns> VColumns { get; set; }

        public virtual DbSet<VTables> VTables { get; set; }

        public virtual DbSet<Versions> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EFCore.Scaffolding.Extension.Connection.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntity>(entity =>
            {
                entity.HasIndex(e => e.OperationId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.AuditEntity)
                    .HasForeignKey(d => d.OperationId);
            });

            modelBuilder.Entity<AuditOperation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AuditProperty>(entity =>
            {
                entity.HasIndex(e => e.AuditEntityId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AuditEntity)
                    .WithMany(p => p.AuditProperty)
                    .HasForeignKey(d => d.AuditEntityId);
            });

            modelBuilder.Entity<EntityInfo>(entity =>
            {
                entity.HasIndex(e => e.TypeName)
                    .HasName("ClassFullNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PropertyJson).IsRequired();

                entity.Property(e => e.TypeName).IsRequired();
            });

            modelBuilder.Entity<EntityRole>(entity =>
            {
                entity.HasComment("数据角色信息");

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => new { e.EntityId, e.RoleId, e.Operation })
                    .HasName("EntityRoleIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.EntityRole)
                    .HasForeignKey(d => d.EntityId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EntityRole)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<EntityUser>(entity =>
            {
                entity.HasComment("数据用户信息");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.EntityId, e.UserId })
                    .HasName("EntityUserIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.EntityUser)
                    .HasForeignKey(d => d.EntityId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EntityUser)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.HasIndex(e => new { e.Area, e.Controller, e.Action })
                    .HasName("AreaControllerActionIndex")
                    .IsUnique()
                    .HasFilter("([Area] IS NOT NULL AND [Controller] IS NOT NULL AND [Action] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<KeyValue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Key).IsRequired();
            });

            modelBuilder.Entity<LoginLog>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginLog)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.SenderId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.SenderId);
            });

            modelBuilder.Entity<MessageReceive>(entity =>
            {
                entity.HasIndex(e => e.MessageId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReceive)
                    .HasForeignKey(d => d.MessageId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReceive)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MessageReply>(entity =>
            {
                entity.HasIndex(e => e.BelongMessageId);

                entity.HasIndex(e => e.ParentMessageId);

                entity.HasIndex(e => e.ParentReplyId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.BelongMessage)
                    .WithMany(p => p.MessageReplyBelongMessage)
                    .HasForeignKey(d => d.BelongMessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ParentMessage)
                    .WithMany(p => p.MessageReplyParentMessage)
                    .HasForeignKey(d => d.ParentMessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ParentReply)
                    .WithMany(p => p.InverseParentReply)
                    .HasForeignKey(d => d.ParentReplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReply)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasComment("模块信息");

                entity.HasIndex(e => e.ParentId);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId);
            });

            modelBuilder.Entity<ModuleFunction>(entity =>
            {
                entity.HasComment("模块功能信息");

                entity.HasIndex(e => e.FunctionId);

                entity.HasIndex(e => new { e.ModuleId, e.FunctionId })
                    .HasName("ModuleFunctionIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.ModuleFunction)
                    .HasForeignKey(d => d.FunctionId);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleFunction)
                    .HasForeignKey(d => d.ModuleId);
            });

            modelBuilder.Entity<ModuleRole>(entity =>
            {
                entity.HasComment("角色模块信息");

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => new { e.ModuleId, e.RoleId })
                    .HasName("ModuleRoleIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleRole)
                    .HasForeignKey(d => d.ModuleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ModuleRole)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<ModuleUser>(entity =>
            {
                entity.HasComment("用户模块信息");

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.ModuleId, e.UserId })
                    .HasName("ModuleUserIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleUser)
                    .HasForeignKey(d => d.ModuleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ModuleUser)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasIndex(e => e.ParentId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.MessageId);

                entity.HasIndex(e => new { e.NormalizedName, e.DeletedTime })
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([DeletedTime] IS NOT NULL)");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.NormalizedName).IsRequired();

                entity.Property(e => e.Remark).HasMaxLength(512);

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.MessageId);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaim)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("用户表");

                entity.HasIndex(e => e.MessageId);

                entity.HasIndex(e => new { e.NormalizeEmail, e.DeletedTime })
                    .HasName("EmailIndex");

                entity.HasIndex(e => new { e.NormalizedUserName, e.DeletedTime })
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([DeletedTime] IS NOT NULL)");

                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.AccessFailedCount).HasComment("登录失败次数");

                entity.Property(e => e.ConcurrencyStamp).HasComment("版本标识");

                entity.Property(e => e.CreatedTime).HasComment("创建时间");

                entity.Property(e => e.DeletedTime).HasComment("数据逻辑删除时间");

                entity.Property(e => e.Email).HasComment("电子邮箱");

                entity.Property(e => e.EmailConfirmed).HasComment("电子邮箱确认");

                entity.Property(e => e.HeadImg).HasComment("用户头像");

                entity.Property(e => e.IsLocked).HasComment("是否锁定");

                entity.Property(e => e.IsSystem).HasComment("是否系统用户");

                entity.Property(e => e.LockoutEnabled).HasComment("是否登录锁");

                entity.Property(e => e.LockoutEnd).HasComment("锁定时间");

                entity.Property(e => e.MessageId).HasComment("Message");

                entity.Property(e => e.NickName).HasComment("NickName");

                entity.Property(e => e.NormalizeEmail).HasComment("标准化的电子邮箱");

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasComment("标准化的用户名");

                entity.Property(e => e.PasswordHash).HasComment("密码哈希值");

                entity.Property(e => e.PhoneNumber).HasComment("手机号码");

                entity.Property(e => e.PhoneNumberConfirmed).HasComment("手机号码确定");

                entity.Property(e => e.Remark).HasComment("备注");

                entity.Property(e => e.SecurityStamp).HasComment("安全标识");

                entity.Property(e => e.TwoFactorEnabled).HasComment("双因子身份验证");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasComment("用户名");

                entity.HasOne(d => d.MessageNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.MessageId);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.ClaimType).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaim)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserDetail)
                    .HasForeignKey<UserDetail>(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("UserLoginIndex")
                    .IsUnique()
                    .HasFilter("([LoginProvider] IS NOT NULL AND [ProviderKey] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogin)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => new { e.UserId, e.RoleId, e.DeletedTime })
                    .HasName("UserRoleIndex")
                    .IsUnique()
                    .HasFilter("([DeletedTime] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("UserTokenIndex")
                    .IsUnique()
                    .HasFilter("([LoginProvider] IS NOT NULL AND [Name] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToken)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<VColumns>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Columns");

                entity.Property(e => e.CharacterMaximumLength).HasColumnName("CHARACTER_MAXIMUM_LENGTH");

                entity.Property(e => e.CharacterOctetLength).HasColumnName("CHARACTER_OCTET_LENGTH");

                entity.Property(e => e.CharacterSetCatalog)
                    .HasColumnName("CHARACTER_SET_CATALOG")
                    .HasMaxLength(128);

                entity.Property(e => e.CharacterSetName)
                    .HasColumnName("CHARACTER_SET_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.CharacterSetSchema)
                    .HasColumnName("CHARACTER_SET_SCHEMA")
                    .HasMaxLength(128);

                entity.Property(e => e.CollationCatalog)
                    .HasColumnName("COLLATION_CATALOG")
                    .HasMaxLength(128);

                entity.Property(e => e.CollationName)
                    .HasColumnName("COLLATION_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.CollationSchema)
                    .HasColumnName("COLLATION_SCHEMA")
                    .HasMaxLength(128);

                entity.Property(e => e.ColumnDefault)
                    .HasColumnName("COLUMN_DEFAULT")
                    .HasMaxLength(4000);

                entity.Property(e => e.ColumnName)
                    .HasColumnName("COLUMN_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.DataType)
                    .HasColumnName("DATA_TYPE")
                    .HasMaxLength(128);

                entity.Property(e => e.DatetimePrecision).HasColumnName("DATETIME_PRECISION");

                entity.Property(e => e.DomainCatalog)
                    .HasColumnName("DOMAIN_CATALOG")
                    .HasMaxLength(128);

                entity.Property(e => e.DomainName)
                    .HasColumnName("DOMAIN_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.DomainSchema)
                    .HasColumnName("DOMAIN_SCHEMA")
                    .HasMaxLength(128);

                entity.Property(e => e.IsNullable)
                    .HasColumnName("IS_NULLABLE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NumericPrecision).HasColumnName("NUMERIC_PRECISION");

                entity.Property(e => e.NumericPrecisionRadix).HasColumnName("NUMERIC_PRECISION_RADIX");

                entity.Property(e => e.NumericScale).HasColumnName("NUMERIC_SCALE");

                entity.Property(e => e.OrdinalPosition).HasColumnName("ORDINAL_POSITION");

                entity.Property(e => e.TableCatalog)
                    .HasColumnName("TABLE_CATALOG")
                    .HasMaxLength(128);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnName("TABLE_NAME")
                    .HasMaxLength(128);

                entity.Property(e => e.TableSchema)
                    .HasColumnName("TABLE_SCHEMA")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<VTables>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Tables");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.KeyCount).HasColumnName("key_count");

                entity.Property(e => e.ModifyDate)
                    .HasColumnName("modify_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128);

                entity.Property(e => e.Rows).HasColumnName("rows");
            });

            modelBuilder.Entity<Versions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.DeletedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
