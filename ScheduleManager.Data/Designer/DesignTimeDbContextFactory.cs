using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ScheduleManager.Data.Common;
using ScheduleManager.Data.Faculties;
using ScheduleManager.Data.Scheduling;

namespace ScheduleManager.Data.Designer
{
    internal class Data
    {
        public const string StubConnectionString = "";
    }

    public class CommonDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CommonContext>
    {
        public CommonContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CommonContext>();
            builder.UseMySql(Data.StubConnectionString);
            return new CommonContext(builder.Options);
        }
    }

    public class FacultyDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FacultyContext>
    {
        public FacultyContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FacultyContext>();
            builder.UseMySql(Data.StubConnectionString);
            return new FacultyContext(builder.Options);
        }
    }

    public class ScheduleDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ScheduleContext>
    {
        public ScheduleContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ScheduleContext>();
            builder.UseMySql(Data.StubConnectionString);
            return new ScheduleContext(builder.Options);
        }
    }
}