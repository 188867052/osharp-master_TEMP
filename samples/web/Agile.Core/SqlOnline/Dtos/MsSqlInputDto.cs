using Liuliu.Demo.Core.Release.Entities;
using OSharp.Identity;
using OSharp.Mapping;

namespace Liuliu.Demo.Core.SqlOnline.Dtos
{
    /// <summary>
    /// 输入DTO：版本信息
    /// </summary>
    [MapTo(typeof(Versions))]
    public class MsSqlInputDto : VersionInputDtoBase<int>
    {
    }
}