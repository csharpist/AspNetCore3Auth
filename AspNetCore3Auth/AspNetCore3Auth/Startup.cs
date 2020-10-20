using AspNetCore3Auth.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore3Auth
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = AppConstants.SimpleCookieAuthenticationScheme;
                })
                .AddScheme<AuthenticationSchemeOptions, SimpleCookieAuthenticationHandler>(AppConstants.SimpleCookieAuthenticationScheme, null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppConstants.AdminClaimPolicy, policy =>
                {
                    policy.RequireClaim(AppConstants.SimpleCookieAuthenticationSchemeRoleClaimType,
                        AppConstants.SimpleCookieAuthenticationSchemeAdminRole);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
