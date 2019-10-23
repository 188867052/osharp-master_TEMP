using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OSharp.Core.Data;
using OSharp.Entity;
using OSharp.Exceptions;
using OSharp.Extensions;
using OSharp.Json;
using OSharp.Reflection;

namespace OSharp.Core.Systems
{
    /// <summary>
    /// 实体类：数据键值对
    /// </summary>
    [Description("键值对信息")]
    public class KeyValue : EntityBase<Guid>, ILockable, IKeyValue
    {
        /// <summary>
        /// 初始化一个<see cref="KeyValue"/>类型的新实例
        /// </summary>
        public KeyValue()
        {
        }

        /// <summary>
        /// 初始化一个<see cref="KeyValue"/>类型的新实例
        /// </summary>
        public KeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// 获取或设置 数据值JSON字符串
        /// </summary>
        [DisplayName("数据值JSON")]
        public string ValueJson { get; set; }

        /// <summary>
        /// 获取或设置 数据值类型
        /// </summary>
        [DisplayName("数据值类型名")]
        public string ValueType { get; set; }

        /// <summary>
        /// 获取或设置 数据键名
        /// </summary>
        [Required]
        [DisplayName("数据键名")]
        public string Key { get; set; }

        /// <summary>
        /// 获取或设置 数据值
        /// </summary>
        [NotMapped]
        [DisplayName("数据值")]
        public object Value
        {
            get
            {
                if (this.ValueJson == null || this.ValueType == null)
                {
                    return null;
                }

                Type type = Type.GetType(this.ValueType);
                if (type == null)
                {
                    throw new OsharpException($"获取Key为“{this.Key}”的字典值时类型“{this.ValueType}”无法获取");
                }

                return this.ValueJson.FromJsonString(type);
            }
            set
            {
                this.ValueType = value?.GetType().GetFullNameWithModule();
                this.ValueJson = value?.ToJsonString();
            }
        }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取强类型数据值
        /// </summary>
        public T GetValue<T>()
        {
            object value = this.Value;
            if (Equals(value, default(T)))
            {
                return default;
            }

            if (value is T)
            {
                return (T)value;
            }

            try
            {
                return value.CastTo<T>();
            }
            catch (Exception)
            {
                throw new OsharpException($"获取强类型字典值时传入类型“{typeof(T)}”与实际数据类型“{this.ValueType}”不匹配");
            }
        }
    }
}