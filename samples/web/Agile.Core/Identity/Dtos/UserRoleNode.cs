using Agile.Core.Identity.Entities;
using OSharp.Entity;
using OSharp.Mapping;

namespace Agile.Core.Identity.Dtos
{
    /// <summary>
    /// 用户角色节点
    /// </summary>
    [MapFrom(typeof(Role))]
    public class UserRoleNode : IOutputDto
    {
        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}