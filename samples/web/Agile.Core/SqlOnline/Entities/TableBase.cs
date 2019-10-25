using System;
using OSharp.Entity;

namespace Liuliu.Demo.Core.Release.Entities
{
    /// <summary>
    /// 表信息基类
    /// </summary>
    public abstract class TableBase<TKey> : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}