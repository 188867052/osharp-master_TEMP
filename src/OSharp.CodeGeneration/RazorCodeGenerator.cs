// -----------------------------------------------------------------------
//  <copyright file="RazorCodeGenerator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2019 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2019-01-07 2:31</last-date>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using OSharp.CodeGeneration.Schema;
using OSharp.Data;
using OSharp.Exceptions;
using OSharp.Extensions;

using RazorEngine;
using RazorEngine.Templating;

namespace OSharp.CodeGeneration
{
    /// <summary>
    /// Razor代码生成器
    /// </summary>
    public class RazorCodeGenerator : ICodeGenerator
    {
        private readonly IDictionary<CodeType, ITemplateKey> _keyDict = new ConcurrentDictionary<CodeType, ITemplateKey>();

        /// <summary>
        /// 生成项目文件
        /// </summary>
        /// <param name="project">项目元数据</param>
        /// <returns>项目代码</returns>
        public virtual CodeFile[] GenerateProjectCode(ProjectMetadata project)
        {
            List<CodeFile> codeFiles = new List<CodeFile>();
            ModuleMetadata[] modules = project.Modules.ToArray();
            EntityMetadata[] entities = modules.SelectMany(m => m.Entities).ToArray();

            codeFiles.AddRange(entities.Select(this.GenerateEntityCode));
            codeFiles.AddRange(entities.Select(this.GenerateInputDtoCode));
            codeFiles.AddRange(entities.Select(this.GenerateOutputDtoCode));
            codeFiles.AddRange(entities.Select(this.GenerateServiceEntityImpl));
            codeFiles.AddRange(entities.Select(this.GenerateEntityConfiguration));
            codeFiles.AddRange(entities.Select(this.GenerateAdminController));

            codeFiles.AddRange(modules.Select(this.GenerateServiceContract));
            codeFiles.AddRange(modules.Select(this.GenerateServiceMainImpl));

            return codeFiles.OrderBy(m => m.FileName).ToArray();
        }

        /// <summary>
        /// 由实体元数据生成实体类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>实体类代码</returns>
        public virtual CodeFile GenerateEntityCode(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.Entity, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.Entity);
                key = this.GetKey(CodeType.Entity, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.Entity, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.Core/{entity.Module.Name}/Entities/{entity.Name}.generated.cs",
            };
        }

        /// <summary>
        /// 由实体元数据生成输入DTO类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>输入DTO类代码</returns>
        public virtual CodeFile GenerateInputDtoCode(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.InputDto, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.InputDto);
                key = this.GetKey(CodeType.InputDto, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.InputDto, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.Core/{entity.Module.Name}/Dtos/{entity.Name}InputDto.generated.cs",
            };
        }

        /// <summary>
        /// 由实体元数据生成输出DTO类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>输出DTO类代码</returns>
        public virtual CodeFile GenerateOutputDtoCode(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.OutputDto, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.OutputDto);
                key = this.GetKey(CodeType.OutputDto, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.OutputDto, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.Core/{entity.Module.Name}/Dtos/{entity.Name}OutputDto.generated.cs",
            };
        }

        /// <summary>
        /// 由模块元数据生成模块业务契约接口代码
        /// </summary>
        /// <param name="module">模块元数据</param>
        /// <returns>模块业务契约接口代码</returns>
        public virtual CodeFile GenerateServiceContract(ModuleMetadata module)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.ServiceContract, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.ServiceContract);
                key = this.GetKey(CodeType.ServiceContract, template);
                code = Engine.Razor.RunCompile(template, key, module.GetType(), module);
                this._keyDict.Add(CodeType.ServiceContract, key);
            }
            else
            {
                code = Engine.Razor.Run(key, module.GetType(), module);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{module.Project.NamespacePrefix}.Core/{module.Name}/I{module.Name}Contract.generated.cs",
            };
        }

        /// <summary>
        /// 由模块元数据生成模块业务综合实现类代码
        /// </summary>
        /// <param name="module">模块元数据</param>
        /// <returns>模块业务综合实现类代码</returns>
        public virtual CodeFile GenerateServiceMainImpl(ModuleMetadata module)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.ServiceMainImpl, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.ServiceMainImpl);
                key = this.GetKey(CodeType.ServiceMainImpl, template);
                code = Engine.Razor.RunCompile(template, key, module.GetType(), module);
                this._keyDict.Add(CodeType.ServiceMainImpl, key);
            }
            else
            {
                code = Engine.Razor.Run(key, module.GetType(), module);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{module.Project.NamespacePrefix}.Core/{module.Name}/{module.Name}Service.generated.cs",
            };
        }

        /// <summary>
        /// 由模块元数据生成模块业务单实体实现类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>模块业务单实体实现类代码</returns>
        public virtual CodeFile GenerateServiceEntityImpl(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.ServiceEntityImpl, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.ServiceEntityImpl);
                key = this.GetKey(CodeType.ServiceEntityImpl, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.ServiceEntityImpl, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.Core/{entity.Module.Name}/{entity.Module.Name}Service.{entity.Name}.generated.cs",
            };
        }

        /// <summary>
        /// 由模块元数据生成实体数据映射配置类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>实体数据映射配置类代码</returns>
        public virtual CodeFile GenerateEntityConfiguration(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.EntityConfiguration, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.EntityConfiguration);
                key = this.GetKey(CodeType.EntityConfiguration, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.EntityConfiguration, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.EntityConfiguration/{entity.Module.Name}/{entity.Name}Configuration.generated.cs",
            };
        }

        /// <summary>
        /// 由模块元数据生成实体管理控制器类代码
        /// </summary>
        /// <param name="entity">实体元数据</param>
        /// <returns>实体管理控制器类代码</returns>
        public virtual CodeFile GenerateAdminController(EntityMetadata entity)
        {
            string code;
            if (!this._keyDict.TryGetValue(CodeType.AdminController, out ITemplateKey key))
            {
                string template = this.GetInternalTemplate(CodeType.AdminController);
                key = this.GetKey(CodeType.AdminController, template);
                code = Engine.Razor.RunCompile(template, key, entity.GetType(), entity);
                this._keyDict.Add(CodeType.AdminController, key);
            }
            else
            {
                code = Engine.Razor.Run(key, entity.GetType(), entity);
            }

            return new CodeFile()
            {
                SourceCode = code,
                FileName = $"{entity.Module.Project.NamespacePrefix}.Web/Areas/Admin/Controllers/{entity.Module.Name}/{entity.Name}Controller.generated.cs",
            };
        }

        private ITemplateKey GetKey(CodeType codeType, string template)
        {
            Check.NotNull(template, nameof(template));

            string md5 = template.ToMd5Hash();
            string name = $"{codeType.ToString()}-{md5}";
            return Engine.Razor.GetKey(name);
        }

        /// <summary>
        /// 读取指定代码类型的内置代码模板
        /// </summary>
        /// <param name="type">代码类型</param>
        /// <returns></returns>
        private string GetInternalTemplate(CodeType type)
        {
            string resName = $"OSharp.CodeGeneration.Templates.{type.ToString()}.cshtml";
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(resName);
            if (stream == null)
            {
                throw new OsharpException($"无法找到“{type.ToString()}”的内置代码模板");
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}