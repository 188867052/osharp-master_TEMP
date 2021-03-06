﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using OSharp.Entity;

namespace Agile.Web.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    partial class DefaultDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.LoginLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Ip");

                    b.Property<DateTime?>("LogoutTime");

                    b.Property<string>("UserAgent");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ParentId");

                    b.Property<string>("Remark");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DeletedTime");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSystem");

                    b.Property<Guid?>("MessageId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedName")
                        .IsRequired();

                    b.Property<string>("Remark")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("NormalizedName", "DeletedTime")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[DeletedTime] IS NOT NULL");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "97313840-7874-47e5-81f2-565613c8cdcc",
                            CreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAdmin = true,
                            IsDefault = false,
                            IsLocked = false,
                            IsSystem = true,
                            Name = "系统管理员",
                            NormalizedName = "系统管理员",
                            Remark = "系统最高权限管理角色"
                        });
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DeletedTime");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("HeadImg");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSystem");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<Guid?>("MessageId");

                    b.Property<string>("NickName");

                    b.Property<string>("NormalizeEmail");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired();

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Remark");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("NormalizeEmail", "DeletedTime")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName", "DeletedTime")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[DeletedTime] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .IsRequired();

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegisterIp");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey")
                        .IsUnique()
                        .HasName("UserLoginIndex")
                        .HasFilter("[LoginProvider] IS NOT NULL AND [ProviderKey] IS NOT NULL");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DeletedTime");

                    b.Property<bool>("IsLocked");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId", "DeletedTime")
                        .IsUnique()
                        .HasName("UserRoleIndex")
                        .HasFilter("[DeletedTime] IS NOT NULL");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "LoginProvider", "Name")
                        .IsUnique()
                        .HasName("UserTokenIndex")
                        .HasFilter("[LoginProvider] IS NOT NULL AND [Name] IS NOT NULL");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BeginDate");

                    b.Property<bool>("CanReply");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DeletedTime");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSended");

                    b.Property<int>("MessageType");

                    b.Property<int>("NewReplyCount");

                    b.Property<int>("SenderId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.MessageReceive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("MessageId");

                    b.Property<int>("NewReplyCount");

                    b.Property<DateTime>("ReadDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageReceive");
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.MessageReply", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BelongMessageId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<DateTime?>("DeletedTime");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsRead");

                    b.Property<Guid>("ParentMessageId");

                    b.Property<Guid>("ParentReplyId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BelongMessageId");

                    b.HasIndex("ParentMessageId");

                    b.HasIndex("ParentReplyId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageReply");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.EntityRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("FilterGroupJson");

                    b.Property<bool>("IsLocked");

                    b.Property<int>("Operation");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("EntityId", "RoleId", "Operation")
                        .IsUnique()
                        .HasName("EntityRoleIndex");

                    b.ToTable("EntityRole");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.EntityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("FilterGroupJson");

                    b.Property<bool>("IsLocked");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("EntityId", "UserId")
                        .HasName("EntityUserIndex");

                    b.ToTable("EntityUser");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("OrderCode");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Remark");

                    b.Property<string>("TreePathString");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "Root",
                            Name = "根节点",
                            OrderCode = 1.0,
                            Remark = "系统根节点",
                            TreePathString = "$1$"
                        });
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleFunction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FunctionId");

                    b.Property<int>("ModuleId");

                    b.HasKey("Id");

                    b.HasIndex("FunctionId");

                    b.HasIndex("ModuleId", "FunctionId")
                        .IsUnique()
                        .HasName("ModuleFunctionIndex");

                    b.ToTable("ModuleFunction");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ModuleId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ModuleId", "RoleId")
                        .IsUnique()
                        .HasName("ModuleRoleIndex");

                    b.ToTable("ModuleRole");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Disabled");

                    b.Property<int>("ModuleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ModuleId", "UserId")
                        .IsUnique()
                        .HasName("ModuleUserIndex");

                    b.ToTable("ModuleUser");
                });

            modelBuilder.Entity("Liuliu.Demo.Systems.Entities.AuditEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EntityKey");

                    b.Property<string>("Name");

                    b.Property<int>("OperateType");

                    b.Property<Guid>("OperationId");

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("AuditEntity");
                });

            modelBuilder.Entity("Liuliu.Demo.Systems.Entities.AuditOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Browser");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("Elapsed");

                    b.Property<string>("FunctionName");

                    b.Property<string>("Ip");

                    b.Property<string>("Message");

                    b.Property<string>("NickName");

                    b.Property<string>("OperationSystem");

                    b.Property<int>("ResultType");

                    b.Property<string>("UserAgent");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("AuditOperation");
                });

            modelBuilder.Entity("Liuliu.Demo.Systems.Entities.AuditProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AuditEntityId");

                    b.Property<string>("DataType");

                    b.Property<string>("DisplayName");

                    b.Property<string>("FieldName");

                    b.Property<string>("NewValue");

                    b.Property<string>("OriginalValue");

                    b.HasKey("Id");

                    b.HasIndex("AuditEntityId");

                    b.ToTable("AuditProperty");
                });

            modelBuilder.Entity("OSharp.Core.EntityInfos.EntityInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AuditEnabled");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PropertyJson")
                        .IsRequired();

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique()
                        .HasName("ClassFullNameIndex");

                    b.ToTable("EntityInfo");
                });

            modelBuilder.Entity("OSharp.Core.Functions.Function", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessType");

                    b.Property<string>("Action");

                    b.Property<string>("Area");

                    b.Property<bool>("AuditEntityEnabled");

                    b.Property<bool>("AuditOperationEnabled");

                    b.Property<int>("CacheExpirationSeconds");

                    b.Property<string>("Controller");

                    b.Property<bool>("IsAccessTypeChanged");

                    b.Property<bool>("IsAjax");

                    b.Property<bool>("IsCacheSliding");

                    b.Property<bool>("IsController");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Area", "Controller", "Action")
                        .IsUnique()
                        .HasName("AreaControllerActionIndex")
                        .HasFilter("[Area] IS NOT NULL AND [Controller] IS NOT NULL AND [Action] IS NOT NULL");

                    b.ToTable("Function");
                });

            modelBuilder.Entity("OSharp.Core.Systems.KeyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("ValueJson");

                    b.Property<string>("ValueType");

                    b.HasKey("Id");

                    b.ToTable("KeyValue");

                    b.HasData(
                        new
                        {
                            Id = new Guid("534d7813-0eea-44cc-b88e-a9cb010c6981"),
                            IsLocked = false,
                            Key = "Site.Name",
                            ValueJson = "\"OSHARP\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        },
                        new
                        {
                            Id = new Guid("977e4bba-97b2-4759-a768-a9cb010c698c"),
                            IsLocked = false,
                            Key = "Site.Description",
                            ValueJson = "\"Osharp with .NetStandard2.0 & Angular6\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        });
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.LoginLog", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.Organization", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.Organization")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.Role", b =>
                {
                    b.HasOne("Liuliu.Demo.Infos.Entities.Message")
                        .WithMany("PublicRoles")
                        .HasForeignKey("MessageId");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.RoleClaim", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.User", b =>
                {
                    b.HasOne("Liuliu.Demo.Infos.Entities.Message")
                        .WithMany("Recipients")
                        .HasForeignKey("MessageId");
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserClaim", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserDetail", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("Liuliu.Demo.Identity.Entities.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserLogin", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserRole", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Identity.Entities.UserToken", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.Message", b =>
                {
                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.MessageReceive", b =>
                {
                    b.HasOne("Liuliu.Demo.Infos.Entities.Message", "Message")
                        .WithMany("Receives")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Liuliu.Demo.Infos.Entities.MessageReply", b =>
                {
                    b.HasOne("Liuliu.Demo.Infos.Entities.Message", "BelongMessage")
                        .WithMany()
                        .HasForeignKey("BelongMessageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Liuliu.Demo.Infos.Entities.Message", "ParentMessage")
                        .WithMany("Replies")
                        .HasForeignKey("ParentMessageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Liuliu.Demo.Infos.Entities.MessageReply", "ParentReply")
                        .WithMany("Replies")
                        .HasForeignKey("ParentReplyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.EntityRole", b =>
                {
                    b.HasOne("OSharp.Core.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.EntityUser", b =>
                {
                    b.HasOne("OSharp.Core.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.Module", b =>
                {
                    b.HasOne("Liuliu.Demo.Security.Entities.Module", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleFunction", b =>
                {
                    b.HasOne("OSharp.Core.Functions.Function", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleRole", b =>
                {
                    b.HasOne("Liuliu.Demo.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Security.Entities.ModuleUser", b =>
                {
                    b.HasOne("Liuliu.Demo.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Liuliu.Demo.Identity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Systems.Entities.AuditEntity", b =>
                {
                    b.HasOne("Liuliu.Demo.Systems.Entities.AuditOperation", "Operation")
                        .WithMany("AuditEntities")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liuliu.Demo.Systems.Entities.AuditProperty", b =>
                {
                    b.HasOne("Liuliu.Demo.Systems.Entities.AuditEntity", "AuditEntity")
                        .WithMany("Properties")
                        .HasForeignKey("AuditEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
