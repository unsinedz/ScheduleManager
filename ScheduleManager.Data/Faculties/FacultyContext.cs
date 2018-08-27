using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Faculties
{
    public class FacultyContext : DbContext
    {
        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public FacultyContext(DbContextOptions<FacultyContext> options) : base(options)
        {
        }

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
                .WithOne(x => x.Faculty)
                .HasForeignKey("FacultyId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Faculty_Department");
        }

        protected virtual void BuildDepartment(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Lecturers)
                .WithOne(x => x.Department)
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Department_Lecturer");
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