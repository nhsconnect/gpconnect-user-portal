using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gpconnect_user_portal.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAggregateService _aggregateService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAggregateService aggregateService, IHttpContextAccessor contextAccessor, ILogger<AuthController> logger)
        {
            _aggregateService = aggregateService;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        [Route("/Auth/Login")]
        [AllowAnonymous]
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("OpenIdConnect", new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
            });
        }

        [Route("/Auth/Logout")]
        public IActionResult Logout()
        {
            //var userSessionId = _contextAccessor.HttpContext.User.GetClaimValue("UserSessionId").StringToInteger(0);
            //try
            //{
            //    if (userSessionId > 0)
            //    {
            //        _aggregateService.LogoffUser(new User
            //        {
            //            EmailAddress = _contextAccessor.HttpContext.User.GetClaimValue("Email"),
            //            UserSessionId = userSessionId
            //        });
            //    }
            //}
            //catch (Exception exc)
            //{
            //    _logger.LogError(exc, "An error occurred in trying to logout the user");
            //    throw;
            //}
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
