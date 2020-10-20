using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

            // В запросе нет cookie с нужным именем
            if (!Request.Cookies.ContainsKey(AppConstants.SimpleCookieAuthenticationSchemeCookieName))
            {
                return AuthenticateResult.Fail("No cookie found");
            }

            // Получаем из cookie имя пользователя
            var username = Request.Cookies[AppConstants.SimpleCookieAuthenticationSchemeCookieName];

            // Собираем множество утверждений для пользователя
            var claims = new List<Claim>
            {
                // Одно из утверждений по имени пользователя
                new Claim(AppConstants.SimpleCookieAuthenticationSchemeUserNameClaimType, username)
            };

            // Имитируем бизнес-логику: если в username значение bob, то добавляем роль
            // администратора, иначе — обычного пользователя
            if (username == "bob")
            {
                claims.Add(new Claim(AppConstants.SimpleCookieAuthenticationSchemeRoleClaimType, 
                    AppConstants.SimpleCookieAuthenticationSchemeAdminRole));
            }
            else
            {
                claims.Add(new Claim(AppConstants.SimpleCookieAuthenticationSchemeRoleClaimType,
                    AppConstants.SimpleCookieAuthenticationSchemeUserRole));
            }

            // Подготавливаем authentication ticket
            var identity = new ClaimsIdentity(claims, AppConstants.SimpleCookieAuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principle, AppConstants.SimpleCookieAuthenticationScheme);
            return AuthenticateResult.Success(ticket);
        }
    }

}