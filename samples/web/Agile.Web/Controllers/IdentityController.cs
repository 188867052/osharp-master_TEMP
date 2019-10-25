using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Agile.Core.Identity;
using Agile.Core.Identity.Dtos;
using Liuliu.Demo.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OSharp.AspNetCore;
using OSharp.AspNetCore.Mvc;
using OSharp.AspNetCore.Mvc.Filters;
using OSharp.AspNetCore.UI;
using OSharp.Core;
using OSharp.Core.Modules;
using OSharp.Data;
using OSharp.Entity;
using OSharp.Extensions;
using OSharp.Filter;
using OSharp.Identity;
using OSharp.Identity.JwtBearer;
using OSharp.Identity.OAuth2;
using OSharp.Json;
using OSharp.Mapping;
using OSharp.Net;
using OSharp.Security.Claims;

namespace Liuliu.Demo.Web.Controllers
{
    [Description("网站-认证")]
    [ModuleInfo(Order = 1)]
    public class IdentityController : ApiController
    {
        private readonly IIdentityContract _identityContract;
        private readonly SignInManager<User> _signInManager;
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly UserManager<User> _userManager;

        public IdentityController(
            IIdentityContract identityContract,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IVerifyCodeService verifyCodeService)
        {
            this._identityContract = identityContract;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._verifyCodeService = verifyCodeService;
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        [Description("用户名是否存在")]
        public bool CheckUserNameExists(string userName)
        {
            return this._userManager.Users.Any(m => m.NormalizedUserName == this._userManager.NormalizeName(userName));
        }

        /// <summary>
        /// 用户Email是否存在
        /// </summary>
        /// <param name="email">电子邮箱</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        [Description("用户Email是否存在")]
        public bool CheckEmailExists(string email)
        {
            return this._userManager.Users.Any(m => m.NormalizeEmail == this._userManager.NormalizeEmail(email));
        }

        /// <summary>
        /// 用户昵称是否存在
        /// </summary>
        /// <param name="nickName">用户昵称</param>
        /// <returns>是否存在</returns>
        [HttpGet]
        [Description("用户昵称是否存在")]
        public async Task<bool> CheckNickNameExists(string nickName)
        {
            var nickNameValidator = this._userManager.UserValidators.FirstOrDefault(m => m.GetType() == typeof(UserNickNameValidator<User, int>));
            if (nickNameValidator == null)
            {
                return false;
            }

            IdentityResult result = await nickNameValidator.ValidateAsync(this._userManager, new User() { NickName = nickName });
            return !result.Succeeded;
        }

        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="dto">注册信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [UnitOfWork]
        [ModuleInfo]
        [DependOnFunction("CheckUserNameExists")]
        [DependOnFunction("CheckEmailExists")]
        [DependOnFunction("CheckNickNameExists")]
        [Description("用户注册")]
        public async Task<AjaxResult> Register(RegisterDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            }

            if (!this._verifyCodeService.CheckCode(dto.VerifyCode, dto.VerifyCodeId))
            {
                return new AjaxResult("验证码错误，请刷新重试", AjaxResultType.Error);
            }

            dto.UserName = dto.Email;
            dto.NickName = $"User_{new Random().NextLetterAndNumberString(8)}"; // 随机用户昵称
            dto.RegisterIp = this.HttpContext.GetClientIp();

            var result = await this._identityContract.Register(dto);
            if (result.Succeeded)
            {
                User user = result.Data;
                string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                code = UrlBase64ReplaceChar(code);
                string url = $"{this.Request.Scheme}://{this.Request.Host}/#/passport/confirm-email?userId={user.Id}&code={code}";
                string body =
                    $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，您好！<br>"
                    + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                    + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                    + $"{url}<br>"
                    + $"祝您使用愉快！";
                await this.SendMailAsync(user.Email, "柳柳软件 注册邮箱激活邮件", body);

                return result.ToAjaxResult(m => new { m.UserName, m.NickName, m.Email });
            }

            return result.ToAjaxResult();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("用户登录")]
        public async Task<AjaxResult> Login(LoginDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            }

            // TODO: 校验验证码
            dto.Ip = this.HttpContext.GetClientIp();
            dto.UserAgent = this.Request.Headers["User-Agent"].FirstOrDefault();

            OperationResult<User> result = await this._identityContract.Login(dto);
            IUnitOfWork unitOfWork = this.HttpContext.RequestServices.GetUnitOfWork<User, int>();
            unitOfWork.Commit();

            if (!result.Succeeded)
            {
                return result.ToAjaxResult();
            }

            User user = result.Data;
            await this._signInManager.SignInAsync(user, dto.Remember);
            return new AjaxResult("登录成功");
        }

        /// <summary>
        /// Jwt登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("JWT登录")]
        public async Task<AjaxResult> Jwtoken(LoginDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            }

            dto.Ip = this.HttpContext.GetClientIp();
            dto.UserAgent = this.Request.Headers["User-Agent"].FirstOrDefault();

            var result = await this._identityContract.Login(dto);
            IUnitOfWork unitOfWork = this.HttpContext.RequestServices.GetUnitOfWork<User, int>();
            unitOfWork.Commit();

            if (!result.Succeeded)
            {
                return result.ToAjaxResult();
            }

            User user = result.Data;
            JsonWebToken token = await this.CreateJwtToken(user);
            return new AjaxResult("登录成功", AjaxResultType.Success, token);
        }

        /// <summary>
        /// 获取身份认证Token
        /// </summary>
        /// <param name="dto">TokenDto</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("JwtToken")]
        public async Task<AjaxResult> Token(TokenDto dto)
        {
            if (dto.GrantType == GrantType.Password)
            {
                Check.NotNull(dto.Account, nameof(dto.Account));
                Check.NotNull(dto.Password, nameof(dto.Password));

                LoginDto loginDto = new LoginDto()
                {
                    Account = dto.Account,
                    Password = dto.Password,
                    Ip = this.HttpContext.GetClientIp(),
                    UserAgent = this.Request.Headers["User-Agent"].FirstOrDefault(),
                };

                var result = await this._identityContract.Login(loginDto);
                IUnitOfWork unitOfWork = this.HttpContext.RequestServices.GetUnitOfWork<User, int>();
                unitOfWork.Commit();
                if (!result.Succeeded)
                {
                    return result.ToAjaxResult();
                }

                User user = result.Data;
                JsonWebToken token = await this.CreateJwtToken(user);
                return new AjaxResult("登录成功", AjaxResultType.Success, token);
            }

            if (dto.GrantType == GrantType.RefreshToken)
            {
                Check.NotNull(dto.RefreshToken, nameof(dto.RefreshToken));
                JsonWebToken token = await this.CreateJwtToken(dto.RefreshToken);
                return new AjaxResult("刷新成功", AjaxResultType.Success, token);
            }

            return new AjaxResult("GrantType错误", AjaxResultType.Error);
        }

        /// <summary>
        /// OAuth2登录
        /// </summary>
        /// <param name="provider">登录提供程序</param>
        /// <param name="returnUrl">登录成功返回URL</param>
        /// <returns></returns>
        [HttpGet]
        [ModuleInfo]
        [Description("OAuth2登录")]
        public IActionResult OAuth2(string provider, string returnUrl = null)
        {
            string redirectUrl = this.Url.Action(nameof(this.OAuth2Callback), "Identity", new { returnUrl });
            AuthenticationProperties properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return this.Challenge(properties, provider);
        }

        /// <summary>
        /// OAuth2登录回调
        /// </summary>
        /// <param name="returnUrl">登录成功返回URL</param>
        /// <param name="remoteError">远程错误信息</param>
        /// <returns></returns>
        [HttpGet]
        [Description("OAuth2登录回调")]
        public async Task<ActionResult> OAuth2Callback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.Logger.LogError($"第三方登录错误：{remoteError}");
                return this.Json(new AjaxResult($"第三方登录错误：{remoteError}", AjaxResultType.UnAuth));
            }

            string url;
            ExternalLoginInfo info = await this._signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                url = "/#/exception/500";
                this.Logger.LogError("第三方登录返回的用户信息为空");
                return this.Redirect(url);
            }

            UserLoginInfoEx loginInfo = info.ToUserLoginInfoEx();
            var result = await this._identityContract.LoginOAuth2(loginInfo);

            // 登录不成功，将用户信息返回前端，让用户选择绑定现有账号还是创建新账号
            if (!result.Succeeded)
            {
                string cacheId = (string)result.Data;
                loginInfo.ProviderKey = cacheId;
                url = $"/#/passport/oauth-callback?type={loginInfo.LoginProvider}&id={cacheId}&name={loginInfo.ProviderDisplayName?.ToUrlEncode()}&avatar={loginInfo.AvatarUrl?.ToUrlEncode()}";
                return this.Redirect(url);
            }

            this.Logger.LogInformation($"用户“{info.Principal.Identity.Name}”通过 {info.ProviderDisplayName} OAuth2登录成功");
            JsonWebToken token = await this.CreateJwtToken((User)result.Data);
            url = $"/#/passport/oauth-callback?token={token.ToJsonString()}";
            return this.Redirect(url);
        }

        /// <summary>
        /// 读取用户列表信息
        /// </summary>
        /// <returns>用户列表信息</returns>
        [HttpPost]
        [LoggedIn]
        [ModuleInfo]
        [Description("读取OAuth2")]
        public PageData<UserLoginOutputDto> ReadOAuth2([FromServices]IFilterService filterService, PageRequest request)
        {
            int userId = this.User.Identity.GetUserId<int>();
            request.FilterGroup.AddRule("UserId", userId);
            request.AddDefaultSortCondition(new SortCondition("LoginProvider"));

            Expression<Func<UserLogin, bool>> exp = filterService.GetExpression<UserLogin>(request.FilterGroup);
            var page = this._identityContract.UserLogins.ToPage<UserLogin, UserLoginOutputDto>(exp, request.PageCondition);
            return page.ToPageData();
        }

        /// <summary>
        /// 登录并绑定账号
        /// </summary>
        [HttpPost]
        [ModuleInfo]
        [Description("登录并绑定账号")]
        [UnitOfWork]
        public async Task<AjaxResult> LoginBind(UserLoginInfoEx loginInfo)
        {
            loginInfo.RegisterIp = this.HttpContext.GetClientIp();
            OperationResult<User> result = await this._identityContract.LoginBind(loginInfo);
            IUnitOfWork unitOfWork = this.HttpContext.RequestServices.GetUnitOfWork<User, int>();
            unitOfWork.Commit();
            if (!result.Succeeded)
            {
                return result.ToAjaxResult();
            }

            User user = result.Data;
            JsonWebToken token = await this.CreateJwtToken(user);
            return new AjaxResult("登录成功", AjaxResultType.Success, token);
        }

        /// <summary>
        /// 使用第三方账号一键登录
        /// </summary>
        [HttpPost]
        [ModuleInfo]
        [Description("第三方一键登录")]
        public async Task<AjaxResult> LoginOneKey(UserLoginInfoEx loginInfo)
        {
            loginInfo.RegisterIp = this.HttpContext.GetClientIp();
            OperationResult<User> result = await this._identityContract.LoginOneKey(loginInfo.ProviderKey);
            IUnitOfWork unitOfWork = this.HttpContext.RequestServices.GetUnitOfWork<User, int>();
            unitOfWork.Commit();

            if (!result.Succeeded)
            {
                return result.ToAjaxResult();
            }

            User user = result.Data;
            JsonWebToken token = await this.CreateJwtToken(user);
            return new AjaxResult("登录成功", AjaxResultType.Success, token);
        }

        private async Task<JsonWebToken> CreateJwtToken(User user)
        {
            IServiceProvider provider = this.HttpContext.RequestServices;
            IJwtBearerService jwtBearerService = provider.GetService<IJwtBearerService>();
            JsonWebToken token = await jwtBearerService.CreateToken(user.Id.ToString(), user.UserName);

            return token;
        }

        private async Task<JsonWebToken> CreateJwtToken(string refreshToken)
        {
            IServiceProvider provider = this.HttpContext.RequestServices;
            IJwtBearerService jwtBearerService = provider.GetService<IJwtBearerService>();
            JsonWebToken token = await jwtBearerService.RefreshToken(refreshToken);
            return token;
        }

        /// <summary>
        /// 解除第三方登录
        /// </summary>
        [HttpPost]
        [LoggedIn]
        [ModuleInfo]
        [Description("解除第三方登录")]
        [UnitOfWork]
        public async Task<AjaxResult> RemoveOAuth2(Guid[] ids)
        {
            OperationResult result = await this._identityContract.DeleteUserLogins(ids);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("用户登出")]
        [UnitOfWork]
        public async Task<AjaxResult> Logout()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return new AjaxResult("用户登出成功");
            }

            int userId = this.User.Identity.GetUserId<int>();
            OperationResult result = await this._identityContract.Logout(userId);
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ModuleInfo]
        [Description("用户信息")]
        public async Task<OnlineUser> Profile()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return null;
            }

            IOnlineUserProvider onlineUserProvider = this.HttpContext.RequestServices.GetService<IOnlineUserProvider>();
            if (onlineUserProvider == null)
            {
                return null;
            }

            OnlineUser onlineUser = await onlineUserProvider.GetOrCreate(this.User.Identity.Name);
            onlineUser.RefreshTokens.Clear();
            return onlineUser;
        }

        /// <summary>
        /// 编辑用户资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoggedIn]
        [ModuleInfo]
        [Description("编辑用户资料")]
        [UnitOfWork]
        public async Task<AjaxResult> ProfileEdit(ProfileEditDto dto)
        {
            int userId = this.User.Identity.GetUserId<int>();
            dto.Id = userId;
            User user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new AjaxResult("用户不存在", AjaxResultType.Error);
            }

            user = dto.MapTo(user);
            var result = await this._userManager.UpdateAsync(user);
            return result.ToOperationResult().ToAjaxResult();
        }

        /// <summary>
        /// 激活邮箱
        /// </summary>
        /// <param name="dto">电子邮箱</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [Description("激活邮箱")]
        [UnitOfWork]
        public async Task<AjaxResult> ConfirmEmail(ConfirmEmailDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("邮箱激活失败：参数不正确", AjaxResultType.Error);
            }

            User user = await this._userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
            {
                return new AjaxResult("注册邮箱激活失败：用户不存在", AjaxResultType.Error);
            }

            if (user.EmailConfirmed)
            {
                return new AjaxResult("注册邮箱已激活，操作取消", AjaxResultType.Info);
            }

            string code = UrlBase64ReplaceChar(dto.Code);
            IdentityResult result = await this._userManager.ConfirmEmailAsync(user, code);
            return result.ToOperationResult().ToAjaxResult();
        }

        /// <summary>
        /// 发送激活注册邮件
        /// </summary>
        /// <param name="dto">激活邮箱信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("CheckEmailExists")]
        [Description("发送激活注册邮件")]
        public async Task<AjaxResult> SendConfirmMail(SendMailDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            }

            if (!this._verifyCodeService.CheckCode(dto.VerifyCode, dto.VerifyCodeId))
            {
                return new AjaxResult("验证码错误，请刷新重试", AjaxResultType.Error);
            }

            User user = await this._userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new AjaxResult("发送激活邮件失败：用户不存在", AjaxResultType.Error);
            }

            if (user.EmailConfirmed)
            {
                return new AjaxResult("Email已激活，无需再次激活", AjaxResultType.Info);
            }

            string code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
            code = UrlBase64ReplaceChar(code);
            string url = $"{this.Request.Scheme}://{this.Request.Host}/#/passport/confirm-email?userId={user.Id}&code={code}";
            string body =
                $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，你好！<br>"
                + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            await this.SendMailAsync(user.Email, "柳柳软件 注册邮箱激活邮件", body);
            return new AjaxResult("激活Email邮件发送成功");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto">修改密码信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [LoggedIn]
        [ModuleInfo]
        [UnitOfWork]
        [Description("修改密码")]
        public async Task<AjaxResult> ChangePassword(ChangePasswordDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            int userId = this.User.Identity.GetUserId<int>();
            User user = await this._userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new AjaxResult($"用户不存在", AjaxResultType.Error);
            }

            IdentityResult result = string.IsNullOrEmpty(dto.OldPassword)
                ? await this._userManager.AddPasswordAsync(user, dto.NewPassword)
                : await this._userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            return result.ToOperationResult().ToAjaxResult();
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        /// <param name="dto">发送邮件信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [DependOnFunction("CheckEmailExists")]
        [Description("发送重置密码邮件")]
        public async Task<AjaxResult> SendResetPasswordMail(SendMailDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return new AjaxResult("提交数据验证失败", AjaxResultType.Error);
            }

            if (!this._verifyCodeService.CheckCode(dto.VerifyCode, dto.VerifyCodeId))
            {
                return new AjaxResult("验证码错误，请刷新重试", AjaxResultType.Error);
            }

            User user = await this._userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new AjaxResult("用户不存在", AjaxResultType.Error);
            }

            string token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            token = UrlBase64ReplaceChar(token);
            IEmailSender sender = this.HttpContext.RequestServices.GetService<IEmailSender>();
            string url = $"{this.Request.Scheme}://{this.Request.Host}/#/passport/reset-password?userId={user.Id}&token={token}";
            string body = $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，您好！<br>"
                + $"欢迎使用柳柳软件账户密码重置功能，请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            await sender.SendEmailAsync(user.Email, "柳柳软件 重置密码邮件", body);
            return new AjaxResult("密码重置邮件发送成功");
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="dto">重置密码信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        [ModuleInfo]
        [UnitOfWork]
        [Description("重置登录密码")]
        public async Task<AjaxResult> ResetPassword(ResetPasswordDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            User user = await this._userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
            {
                return new AjaxResult($"用户不存在", AjaxResultType.Error);
            }

            string token = UrlBase64ReplaceChar(dto.Token);
            IdentityResult result = await this._userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            return result.ToOperationResult().ToAjaxResult();
        }

        private async Task SendMailAsync(string email, string subject, string body)
        {
            IEmailSender sender = this.HttpContext.RequestServices.GetService<IEmailSender>();
            await sender.SendEmailAsync(email, subject, body);
        }

        private static string UrlBase64ReplaceChar(string source)
        {
            if (source.Contains('+') || source.Contains('/'))
            {
                return source.Replace('+', '-').Replace('/', '_');
            }

            if (source.Contains('-') || source.Contains('_'))
            {
                return source.Replace('-', '+').Replace('_', '/');
            }

            return source;
        }
    }
}