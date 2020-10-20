using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore3Auth.Infrastructure
{
    public class SimpleCookieAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public SimpleCookieAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.CompletedTask;

            if (!Request.Cookies.ContainsKey(AppConstants.SimpleCookieAuthenticationSchemeCookieName))
            {
                return AuthenticateResult.Fail("No cookie found");
            }

            var username = Request.Cookies[AppConstants.SimpleCookieAuthenticationSchemeCookieName];

            var claims = new List<Claim>
            {
                new Claim(AppConstants.SimpleCookieAuthenticationSchemeUserNameClaimType, username)
            };

            var identity = new ClaimsIdentity(claims, AppConstants.SimpleCookieAuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principle, AppConstants.SimpleCookieAuthenticationScheme);
            return AuthenticateResult.Success(ticket);
        }
    }
}