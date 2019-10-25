using System.ComponentModel.DataAnnotations;

namespace Agile.Core.Identity.Dtos
{
    /// <summary>
    /// 确认邮箱DTO
    /// </summary>
    public class ConfirmEmailDto
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 邮件码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}