using System;

namespace ScheduleManager.Authentication
{
    public class AuthenticationModuleOptions
    {
        public string DatabaseConnectionString { get; set; }

        public string LoginPath { get; set; }

        public string LogoutPath { get; set; }

        public string AccessDeniedPath { get; set; }

        public bool UseSlidingExpiration { get; set; }

        public TimeSpan Expiration { get; set; }

        public AuthenticationCookieOptions Cookie { get; set; }
    }
}