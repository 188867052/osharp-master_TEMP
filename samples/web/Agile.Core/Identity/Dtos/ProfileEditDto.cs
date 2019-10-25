using System.ComponentModel.DataAnnotations;
using Liuliu.Demo.Identity.Entities;
using OSharp.Entity;
using OSharp.Mapping;

namespace Agile.Core.Identity.Dtos
{
    /// <summary>
    /// 输入DTO：用户资料编辑
    /// </summary>
    [MapTo(typeof(User))]
    public class ProfileEditDto : IInputDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}