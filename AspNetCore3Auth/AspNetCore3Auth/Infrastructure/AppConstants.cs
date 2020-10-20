namespace AspNetCore3Auth.Infrastructure
{
    public static class AppConstants
    {
        public const string SimpleCookieAuthenticationScheme = "SimpleCookieAuthenticationScheme";
        public const string SimpleCookieAuthenticationSchemeCookieName = ".simple.auth";
        public const string SimpleCookieAuthenticationSchemeUserNameClaimType = "scas.username";
        public const string SimpleCookieAuthenticationSchemeRoleClaimType = "scas.role";

        public const string SimpleCookieAuthenticationSchemeAdminRole = "admin";
        public const string SimpleCookieAuthenticationSchemeUserRole = "user";

        public const string AdminClaimPolicy = "MustBeAdmin";
    }
}