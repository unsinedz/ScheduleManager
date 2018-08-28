using System;

namespace ScheduleManager.Authentication
{
    public class AuthenticationCookieOptions
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsSecure { get; set; }

        public TimeSpan? Expiration { get; set; }
    }
}