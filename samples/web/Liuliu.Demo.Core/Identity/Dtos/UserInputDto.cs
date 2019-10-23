using Liuliu.Demo.Identity.Entities;
using OSharp.Identity;
using OSharp.Mapping;

namespace Liuliu.Demo.Identity.Dtos
{
    /// <summary>
    /// 输入DTO：用户信息
    /// </summary>
    [MapTo(typeof(User))]
    public class UserInputDto : UserInputDtoBase<int>
    {
    }
}