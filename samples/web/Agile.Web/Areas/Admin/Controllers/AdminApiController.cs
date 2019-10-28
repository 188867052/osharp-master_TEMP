using OSharp.AspNetCore.Mvc;
using OSharp.Core;

namespace Agile.Web.Areas.Admin.Controllers
{
    [AreaInfo("Admin", Display = "管理")]
    [RoleLimit]
    public abstract class AdminApiController : AreaApiController
    {
    }
}