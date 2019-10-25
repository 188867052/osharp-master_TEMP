using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Agile.Core.Identity;
using Agile.Core.Identity.Dtos;
using Liuliu.Demo.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OSharp.AspNetCore;
using OSharp.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc.Filters;
using OSharp.Collections;
using OSharp.Data;
using OSharp.Entity;

namespace Agile.Web.Controllers
{
    [Description("网站-测试")]
    [ClassFilter]
    public class TestController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityContract _identityContract;

        public TestController(UserManager<User> userManager, IIdentityContract identityContract)
        {
            this._userManager = userManager;
            this._identityContract = identityContract;
        }

        [HttpGet]
        [UnitOfWork]
        [MethodFilter]
        [Description("测试01")]
        public async Task<string> Test01()
        {
            List<object> list = new List<object>();

            if (!this._userManager.Users.Any())
            {
                RegisterDto dto = new RegisterDto
                {
                    UserName = "admin",
                    Password = "osharp123456",
                    ConfirmPassword = "osharp123456",
                    Email = "i66soft@qq.com",
                    NickName = "大站长",
                    RegisterIp = this.HttpContext.GetClientIp(),
                };

                OperationResult<User> result = await this._identityContract.Register(dto);
                if (result.Succeeded)
                {
                    User user = result.Data;
                    user.EmailConfirmed = true;
                    await this._userManager.UpdateAsync(user);
                }

                list.Add(result.Message);

                dto = new RegisterDto()
                {
                    UserName = "osharp",
                    Password = "osharp123456",
                    Email = "mf.guo@qq.com",
                    NickName = "测试号",
                    RegisterIp = this.HttpContext.GetClientIp(),
                };
                result = await this._identityContract.Register(dto);
                if (result.Succeeded)
                {
                    User user = result.Data;
                    user.EmailConfirmed = true;
                    await this._userManager.UpdateAsync(user);
                }

                list.Add(result.Message);
            }

            return list.ExpandAndToString("\r\n");
        }
    }

    public class ClassFilter : ActionFilterAttribute, IExceptionFilter
    {
        private ILogger _logger;

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger = context.HttpContext.RequestServices.GetLogger<ClassFilter>();
            this._logger.LogInformation("ClassFilter - OnActionExecuting");
        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogInformation("ClassFilter - OnActionExecuted");
        }

        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            this._logger.LogInformation("ClassFilter - OnResultExecuting");
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            this._logger.LogInformation("ClassFilter - OnResultExecuted");
        }

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception.Message;
            this._logger.LogInformation("ClassFilter - OnException");
        }
    }

    public class MethodFilter : ActionFilterAttribute
    {
        private ILogger _logger;

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger = context.HttpContext.RequestServices.GetLogger<MethodFilter>();
            this._logger.LogInformation("MethodFilter - OnActionExecuting");
        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogInformation("MethodFilter - OnActionExecuted");
        }

        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            this._logger.LogInformation("MethodFilter - OnResultExecuting");
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            this._logger.LogInformation("MethodFilter - OnResultExecuted");
        }
    }
}