using System.Linq;
using AspNetCore3Auth.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3Auth.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult GenerateCookie(string username)
        {
            Response.Cookies.Append(AppConstants.SimpleCookieAuthenticationSchemeCookieName, username);
            return Content("Cookie has been planted.");
        }

        [Authorize(AuthenticationSchemes = AppConstants.SimpleCookieAuthenticationScheme)]
        public IActionResult TestCookie()
        {
            return Content(string.Join(", ", User.Claims.Select(_ => $"{_.Type} = {_.Value}")));
        }

        [Authorize(AuthenticationSchemes = AppConstants.SimpleCookieAuthenticationScheme, Policy = AppConstants.AdminClaimPolicy)]
        public IActionResult TestAdmin()
        {
            return Content($"Access to {nameof(TestAdmin)} granted! :)");
        }
    }
}
