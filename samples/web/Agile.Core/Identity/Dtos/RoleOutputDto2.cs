using Agile.Core.Identity.Entities;
using OSharp.Entity;
using OSharp.Mapping;

namespace Agile.Core.Identity.Dtos
{
    /// <summary>
    /// 简单角色输出DTO
    /// </summary>
    [MapFrom(typeof(Role))]
    public class RoleOutputDto2 : IOutputDto
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
        /// 获取或设置 角色备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否管理
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}