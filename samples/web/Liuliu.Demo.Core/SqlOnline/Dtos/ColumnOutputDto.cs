using System;
using Entities;
using Liuliu.Demo.Identity.Dtos;
using OSharp.Entity;
using OSharp.Mapping;

namespace Liuliu.Demo.Core.SqlOnline.Dtos
{
    /// <summary>
    /// 输出DTO:字段信息
    /// </summary>
    [MapFrom(typeof(VColumns))]
    public class ColumnOutputDto : IOutputDto
    {
        /// <summary>
        /// 初始化一个<see cref="ColumnOutputDto"/>类型的新实例
        /// </summary>
        public ColumnOutputDto(VColumns u)
        {
            this.OrdinalPosition = u.OrdinalPosition;
            this.TableName = u.TableName;
            this.ColumnName = u.ColumnName;
            this.IsNullable = u.IsNullable;
            this.DataType = u.DataType;
        }

        public string TableCatalog { get; set; }

        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public int? OrdinalPosition { get; set; }

        public string ColumnDefault { get; set; }

        public string IsNullable { get; set; }

        public string DataType { get; set; }

        public int? CharacterMaximumLength { get; set; }

        public int? CharacterOctetLength { get; set; }

        public byte? NumericPrecision { get; set; }

        public short? NumericPrecisionRadix { get; set; }

        public int? NumericScale { get; set; }

        public short? DatetimePrecision { get; set; }

        public string CharacterSetCatalog { get; set; }

        public string CharacterSetSchema { get; set; }

        public string CharacterSetName { get; set; }

        public string CollationCatalog { get; set; }

        public string CollationSchema { get; set; }

        public string CollationName { get; set; }

        public string DomainCatalog { get; set; }

        public string DomainSchema { get; set; }

        public string DomainName { get; set; }

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