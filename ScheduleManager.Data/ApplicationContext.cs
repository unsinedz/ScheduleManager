using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManager.Data.Scheduling.Relations;
using ScheduleManager.Data.ValueConversions;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data
{
    public class ApplicationContext : DbContext
    {
        #region Common

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TimePeriod> TimePeriods { get; set; }

        #endregion

        #region Schedule

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ScheduleGroup> Scheduling { get; set; }

        public DbSet<WeekSchedule> WeekScheduling { get; set; }

        public DbSet<DaySchedule> DayScheduling { get; set; }

        #endregion

        #region Faculty

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        #endregion

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Common
            var commonBuilder = new CommonBuilder();
            modelBuilder.Entity<Attendee>(commonBuilder.BuildAttendee);
            modelBuilder.Entity<Course>(commonBuilder.BuildCourse);
            modelBuilder.Entity<Room>(commonBuilder.BuildRoom);
            modelBuilder.Entity<Subject>(commonBuilder.BuildSubject);
            modelBuilder.Entity<TimePeriod>(commonBuilder.BuildTimePeriod);

            //Faculty
            var facultyBuilder = new FacultyBuilder();
            modelBuilder.Entity<Faculty>(facultyBuilder.BuildFaculty);
            modelBuilder.Entity<Department>(facultyBuilder.BuildDepartment);
            modelBuilder.Entity<Lecturer>(facultyBuilder.BuildLecturer);

            // Schedule
            var scheduleBuilder = new ScheduleBuilder();
            modelBuilder.Entity<Activity>(scheduleBuilder.BuildActivity);
            modelBuilder.Entity<ScheduleGroup>(scheduleBuilder.BuildSchedule);
            modelBuilder.Entity<DaySchedule>(scheduleBuilder.BuildDaySchedule);
            modelBuilder.Entity<WeekSchedule>(scheduleBuilder.BuildWeekSchedule);
            scheduleBuilder.BuildIntermediateRelations(modelBuilder);
        }

        protected class CommonBuilder
        {
            public virtual void BuildAttendee(EntityTypeBuilder<Attendee> builder)
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
                    .IsRequired(false)
                    .HasConstraintName("FK_Course_Attendee");

                builder.HasOne(x => x.Faculty)
                    .WithMany()
                    .HasForeignKey("FacultyId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_Faculty_Attendee");
            }

            public virtual void BuildCourse(EntityTypeBuilder<Course> builder)
            {
                builder.ToTable("Courses");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            }

            public virtual void BuildRoom(EntityTypeBuilder<Room> builder)
            {
                builder.ToTable("Rooms");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            }

            public virtual void BuildSubject(EntityTypeBuilder<Subject> builder)
            {
                builder.ToTable("Subjects");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            }

            public virtual void BuildTimePeriod(EntityTypeBuilder<TimePeriod> builder)
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

        protected class ScheduleBuilder
        {
            public virtual void BuildIntermediateRelations(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ActivityDaySchedule>(BuildActivityToDayScheduleRelation);
                modelBuilder.Entity<DayScheduleWeekSchedule>(BuildDayScheduleToWeekScheduleRelation);
                modelBuilder.Entity<WeekScheduleSchedule>(BuildWeekScheduleToScheduleRelation);
                modelBuilder.Entity<ActivityAttendee>(BuildActivityToAttendeeRelation);
            }

            public virtual void BuildActivityToAttendeeRelation(EntityTypeBuilder<ActivityAttendee> builder)
            {
                builder.ToTable("Activity_Attendee");
                builder.HasKey(x => new { x.ActivityId, x.AttendeeId });
                builder.HasOne(x => x.Activity)
                    .WithMany()
                    .HasForeignKey(x => x.ActivityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Activity__Activity_Attendee");

                builder.HasOne(x => x.Attendee)
                    .WithMany()
                    .HasForeignKey(x => x.AttendeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Attendee__Activity_Attendee");
            }

            public virtual void BuildActivityToDayScheduleRelation(EntityTypeBuilder<ActivityDaySchedule> builder)
            {
                builder.ToTable("Activity_DaySchedule");
                builder.HasKey(x => new { x.ActivityId, x.DayScheduleId });
                builder.HasOne(x => x.Activity)
                    .WithMany()
                    .HasForeignKey(x => x.ActivityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Activity__Activity_DaySchedule");

                builder.HasOne(x => x.DaySchedule)
                    .WithMany()
                    .HasForeignKey(x => x.DayScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DaySchedule__Activity_DaySchedule");
            }

            public virtual void BuildDayScheduleToWeekScheduleRelation(EntityTypeBuilder<DayScheduleWeekSchedule> builder)
            {
                builder.ToTable("DaySchedule_WeekSchedule");
                builder.HasKey(x => new { x.DayScheduleId, x.WeekScheduleId });
                builder.HasOne(x => x.DaySchedule)
                    .WithMany()
                    .HasForeignKey(x => x.DayScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DaySchedule__DaySchedule_WeekSchedule");

                builder.HasOne(x => x.WeekSchedule)
                    .WithMany()
                    .HasForeignKey(x => x.WeekScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__WeekSchedule__DaySchedule_WeekSchedule");
            }

            public virtual void BuildWeekScheduleToScheduleRelation(EntityTypeBuilder<WeekScheduleSchedule> builder)
            {
                builder.ToTable("WeekSchedule_ScheduleGroup");
                builder.HasKey(x => new { x.WeekScheduleId, x.ScheduleGroupId });
                builder.HasOne(x => x.WeekSchedule)
                    .WithMany()
                    .HasForeignKey(x => x.WeekScheduleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__WeekSchedule__WeekSchedule_ScheduleGroup");

                builder.HasOne(x => x.ScheduleGroup)
                    .WithMany()
                    .HasForeignKey(x => x.ScheduleGroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ScheduleGroup__WeekSchedule_ScheduleGroup");
            }

            public virtual void BuildActivity(EntityTypeBuilder<Activity> builder)
            {
                builder.ToTable("Activities");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.HasOne(x => x.TimePeriod)
                    .WithMany()
                    .HasForeignKey("TimePeriodId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_TimePeriod_Activity");

                builder.HasOne(x => x.Subject)
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_Subject_Activity");

                builder.HasOne(x => x.Room)
                    .WithMany()
                    .HasForeignKey("RoomId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_Room_Activity");

                builder.HasOne(x => x.Lecturer)
                    .WithMany()
                    .HasForeignKey("LecturerId")
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false)
                    .HasConstraintName("FK_Lecturer_Activity");
            }

            public virtual void BuildSchedule(EntityTypeBuilder<ScheduleGroup> builder)
            {
                builder.ToTable("ScheduleGroups");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.StartDate)
                    .IsRequired()
                    .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));
            }

            public virtual void BuildDaySchedule(EntityTypeBuilder<DaySchedule> builder)
            {
                builder.ToTable("DayScheduling");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.DayOfWeek)
                    .IsRequired();

                builder.Property(x => x.DedicatedDate)
                    .IsRequired(false)
                    .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));
            }

            public virtual void BuildWeekSchedule(EntityTypeBuilder<WeekSchedule> builder)
            {
                builder.ToTable("WeekScheduling");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.WeekNumber)
                    .IsRequired();
            }
        }

        protected class FacultyBuilder
        {
            public virtual void BuildFaculty(EntityTypeBuilder<Faculty> builder)
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
                    .IsRequired(false)
                    .HasConstraintName("FK_Faculty_Department");
            }

            public virtual void BuildDepartment(EntityTypeBuilder<Department> builder)
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
                    .IsRequired(false)
                    .HasConstraintName("FK_Department_Lecturer");
            }

            public virtual void BuildLecturer(EntityTypeBuilder<Lecturer> builder)
            {
                builder.ToTable("Lecturers");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            }
        }
    }
}