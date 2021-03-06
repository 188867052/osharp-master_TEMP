// -----------------------------------------------------------------------
//  <copyright file="PropertyMetadata.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-08-06 12:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using OSharp.Reflection;

namespace OSharp.CodeGenerator
{
    /// <summary>
    /// 属性元数据
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// 初始化一个<see cref="PropertyMetadata"/>类型的新实例
        /// </summary>
        public PropertyMetadata()
        {
        }

        /// <summary>
        /// 初始化一个<see cref="PropertyMetadata"/>类型的新实例
        /// </summary>
        public PropertyMetadata(PropertyInfo property)
        {
            if (property == null)
            {
                return;
            }

            this.Name = property.Name;
            this.TypeName = property.PropertyType.FullName;
            this.Display = property.GetDescription();
            RequiredAttribute required = property.GetAttribute<RequiredAttribute>();
            if (required != null)
            {
                this.IsRequired = !required.AllowEmptyStrings;
            }

            StringLengthAttribute stringLength = property.GetAttribute<StringLengthAttribute>();
            if (stringLength != null)
            {
                this.MaxLength = stringLength.MaximumLength;
                this.MinLength = stringLength.MinimumLength;
            }
            else
            {
                this.MaxLength = property.GetAttribute<MaxLengthAttribute>()?.Length;
                this.MinLength = property.GetAttribute<MinLengthAttribute>()?.Length;
            }

            RangeAttribute range = property.GetAttribute<RangeAttribute>();
            if (range != null)
            {
                this.Range = new[] { range.Minimum, range.Maximum };
                this.Max = range.Maximum;
                this.Min = range.Minimum;
            }

            this.IsNullable = property.PropertyType.IsNullableType();
            if (this.IsNullable)
            {
                this.TypeName = property.PropertyType.GetUnNullableType().FullName;
            }

            // 枚举类型，作为数值类型返回
            if (property.PropertyType.IsEnum)
            {
                Type enumType = property.PropertyType;
                Array values = enumType.GetEnumValues();
                Enum[] enumItems = values.Cast<Enum>().ToArray();
                if (enumItems.Length > 0)
                {
                    this.EnumMetadatas = enumItems.Select(m => new EnumMetadata(m)).ToArray();
                }
            }
        }

        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 属性类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 获取或设置 是否必须
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 获取或设置 最大长度
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// 获取或设置 最小长度
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// 获取或设置 取值范围
        /// </summary>
        public object[] Range { get; set; }

        /// <summary>
        /// 获取或设置 最大值
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// 获取或设置 最小值
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// 获取或设置 是否值类型可空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 获取或设置 枚举元数据
        /// </summary>
        public EnumMetadata[] EnumMetadatas { get; set; }

        /// <summary>
        /// 是否有验证属性
        /// </summary>
        public bool HasValidateAttribute()
        {
            return this.IsRequired.HasValue || this.MaxLength.HasValue || this.MinLength.HasValue || this.Range != null || this.Max != null || this.Min != null;
        }
    }
}