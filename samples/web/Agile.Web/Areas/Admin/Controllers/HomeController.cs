using System.ComponentModel;
using Liuliu.Demo.Security;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Web.Areas.Admin.Controllers
{
    [Description("管理-主页")]
    public class HomeController : AdminApiController
    {
        private readonly SecurityManager _securityManager;

        public HomeController(SecurityManager securityManager)
        {
            this._securityManager = securityManager;
        }

        /// <summary>
        /// 获取后台管理主菜单
        /// </summary>
        /// <returns>菜单信息</returns>
        [HttpGet]
        [Description("主菜单")]
        public ActionResult MainMenu()
        {
            return this.Content("MainMenu");
        }
    }
}