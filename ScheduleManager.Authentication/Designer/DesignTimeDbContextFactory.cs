using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ScheduleManager.Authentication.Storage;

namespace ScheduleManager.Authentication.Designer
{
    internal class Data
    {
        public const string StubConnectionString = "Server=localhost;Database=schedule_manager;Uid=root;Pwd=root;";
    }
    
    public class IdentityDesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseMySql(Data.StubConnectionString);
            return new IdentityContext(builder.Options);
        }
    }
}