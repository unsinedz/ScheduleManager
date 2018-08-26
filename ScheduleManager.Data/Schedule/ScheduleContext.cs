using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManager.Data.Generators;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Data.Schedule
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Faculty>(BuildFaculty);
            builder.Entity<Department>(BuildDepartment);
            builder.Entity<Lecturer>(BuildLecturer);
        }

        protected virtual void BuildFaculty(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("Faculties");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Departments)
                .WithOne(x => x.Faculty);
        }

        protected virtual void BuildDepartment(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Lecturers);
            builder.HasOne(x => x.Faculty);
        }

        protected virtual void BuildLecturer(EntityTypeBuilder<Lecturer> builder)
        {
            builder.ToTable("Lecturers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}