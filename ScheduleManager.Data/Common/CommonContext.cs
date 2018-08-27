using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManager.Data.ValueConversions;
using ScheduleManager.Domain.Common;

namespace ScheduleManager.Data.Common
{
    public class CommonContext : DbContext
    {
        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TimePeriod> TimePeriods { get; set; }

        public CommonContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>(BuildAttendee);
            modelBuilder.Entity<Course>(BuildCourse);
            modelBuilder.Entity<Room>(BuildRoom);
            modelBuilder.Entity<Subject>(BuildSubject);
            modelBuilder.Entity<TimePeriod>(BuildTimePeriod);
        }

        protected virtual void BuildAttendee(EntityTypeBuilder<Attendee> builder)
        {
            builder.ToTable("Attendees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AttendeeType)
                .IsRequired()
                .HasConversion(x => (int)x, x => (AttendeeType)x);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey("CourseId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Course_Attendee");

            builder.HasOne(x => x.Faculty)
                .WithMany()
                .HasForeignKey("FacultyId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey("FK_Faculty_Attendee");
        }

        protected virtual void BuildCourse(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
        }

        protected virtual void BuildRoom(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
        }

        protected virtual void BuildSubject(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
        }

        protected virtual void BuildTimePeriod(EntityTypeBuilder<TimePeriod> builder)
        {
            builder.ToTable("TimePeriods");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Start)
                .IsRequired()
                .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));

            builder.Property(x => x.End)
                .IsRequired()
                .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));
        }
    }
}