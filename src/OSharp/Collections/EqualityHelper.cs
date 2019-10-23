// -----------------------------------------------------------------------
//  <copyright file="EqualityHelper.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-07-18 16:02</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace OSharp.Collections
{
    /// <summary>
    /// 相等比较辅助类，用于快速创建<see cref="IEqualityComparer{T}"/>的实例
    /// </summary>
    /// <example>
    /// var equalityComparer1 = EqualityHelper[Person].CreateComparer(p => p.ID);
    /// var equalityComparer2 = EqualityHelper[Person].CreateComparer(p => p.Name);
    /// var equalityComparer3 = EqualityHelper[Person].CreateComparer(p => p.Birthday.Year);
    /// </example>
    /// <typeparam name="T"> </typeparam>
    public static class EqualityHelper<T>
    {
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>的实例
        /// </summary>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector)
        {
            return new CommonEqualityComparer<TV>(keySelector);
        }

        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>与结果二次比较器<paramref name="comparer"/>的实例
        /// </summary>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            return new CommonEqualityComparer<TV>(keySelector, comparer);
        }

        private class CommonEqualityComparer<TV> : IEqualityComparer<T>
        {
            private readonly IEqualityComparer<TV> _comparer;
            private readonly Func<T, TV> _keySelector;

            public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
            {
                this._keySelector = keySelector;
                this._comparer = comparer;
            }

            public CommonEqualityComparer(Func<T, TV> keySelector)
                : this(keySelector, EqualityComparer<TV>.Default)
            {
            }

            public bool Equals(T x, T y)
            {
                return this._comparer.Equals(this._keySelector(x), this._keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return this._comparer.GetHashCode(this._keySelector(obj));
            }
        }
    }
}