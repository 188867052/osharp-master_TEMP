using System;
using Agile.Core.Identity.Dtos;
using Liuliu.Demo.Core.Release.Entities;
using OSharp.Entity;
using OSharp.Mapping;

namespace Liuliu.Demo.Identity.Dtos
{
    /// <summary>
    /// 输出DTO:版本信息
    /// </summary>
    [MapFrom(typeof(Versions))]
    public class VersionOutputDto : IOutputDto, IDataAuthEnabled
    {
        /// <summary>
        /// 初始化一个<see cref="UserOutputDto"/>类型的新实例
        /// </summary>
        public VersionOutputDto(Versions u)
        {
            this.Id = u.Id;
            this.CreatedTime = u.CreatedTime;
            this.Name = u.Name;
        }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 版本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 是否可更新
        /// </summary>
        public bool Updatable { get; set; }

        /// <summary>
        /// 获取或设置 是否可删除
        /// </summary>
        public bool Deletable { get; set; }
    }
}