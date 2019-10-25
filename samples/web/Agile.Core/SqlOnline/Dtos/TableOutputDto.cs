using System;
using System.Linq;
using Agile.Core.Identity.Dtos;
using Entities;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using OSharp.Entity;
using OSharp.Mapping;

namespace Liuliu.Demo.Core.SqlOnline.Dtos
{
    /// <summary>
    /// 输出DTO:版本信息
    /// </summary>
    [MapFrom(typeof(VTables))]
    public class TableOutputDto : IOutputDto
    {
        /// <summary>
        /// 初始化一个<see cref="UserOutputDto"/>类型的新实例
        /// </summary>
        public TableOutputDto(VTables u, DatabaseTable databaseTable)
        {
            this.Name = u.Name;
            this.CreateDate = u.CreateDate;
            this.ModifyDate = u.ModifyDate;
            this.ColumnCount = databaseTable.Columns.Count;
            this.Comment = databaseTable.Comment;
            this.PrimaryKeys = string.Join(",", databaseTable.PrimaryKey.Columns.Select(o => o.Name));
            this.Rows = u.Rows;
        }

        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        public string Name { get; set; }

        public string Comment { get; set; }

        public string PrimaryKeys { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public int? Rows { get; set; }

        public int ColumnCount { get; set; }

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