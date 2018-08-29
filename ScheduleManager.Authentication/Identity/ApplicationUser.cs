using System;
using Microsoft.AspNetCore.Identity;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Authentication.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}