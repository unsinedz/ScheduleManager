using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Authentication.Identity;

namespace ScheduleManager.Authentication.Storage
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, UserRole, Guid>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}