using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManager.Data.Scheduling.Relations;
using ScheduleManager.Data.ValueConversions;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling
{
    public class ScheduleContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public DbSet<ScheduleGroup> Scheduling { get; set; }

        public DbSet<WeekSchedule> WeekScheduling { get; set; }

        public DbSet<DaySchedule> DayScheduling { get; set; }

        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.BuildIntermediateRelations(modelBuilder);
            modelBuilder.Entity<Activity>(BuildActivity);
            modelBuilder.Entity<ScheduleGroup>(BuildSchedule);
            modelBuilder.Entity<DaySchedule>(BuildDaySchedule);
            modelBuilder.Entity<WeekSchedule>(BuildWeekSchedule);
        }

        protected virtual void BuildIntermediateRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityDaySchedule>(BuildActivityToDayScheduleRelation);
            modelBuilder.Entity<DayScheduleWeekSchedule>(BuildDayScheduleToWeekScheduleRelation);
            modelBuilder.Entity<WeekScheduleSchedule>(BuildWeekScheduleToScheduleRelation);
        }

        protected virtual void BuildActivityToDayScheduleRelation(EntityTypeBuilder<ActivityDaySchedule> builder)
        {
            builder.ToTable("Activity_DaySchedule");
            builder.HasKey(x => new { x.ActivityId, x.DayScheduleId });
            builder.HasOne(x => x.Activity)
                .WithMany()
                .HasForeignKey(x => x.ActivityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Activity__Activity_DaySchedule");
            
            builder.HasOne(x => x.DaySchedule)
                .WithMany()
                .HasForeignKey(x => x.DayScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__DaySchedule__Activity_DaySchedule");
        }

        protected virtual void BuildDayScheduleToWeekScheduleRelation(EntityTypeBuilder<DayScheduleWeekSchedule> builder)
        {
            builder.ToTable("DaySchedule_WeekSchedule");
            builder.HasKey(x => new { x.DayScheduleId, x.WeekScheduleId });            
            builder.HasOne(x => x.DaySchedule)
                .WithMany()
                .HasForeignKey(x => x.DayScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__DaySchedule__DaySchedule_WeekSchedule");
                
            builder.HasOne(x => x.WeekSchedule)
                .WithMany()
                .HasForeignKey(x => x.WeekScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__WeekSchedule__DaySchedule_WeekSchedule");
        }

        protected virtual void BuildWeekScheduleToScheduleRelation(EntityTypeBuilder<WeekScheduleSchedule> builder)
        {
            builder.ToTable("WeekSchedule_ScheduleGroup");
            builder.HasKey(x => new { x.WeekScheduleId, x.ScheduleGroupId });
            builder.HasOne(x => x.WeekSchedule)
                .WithMany()
                .HasForeignKey(x => x.WeekScheduleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__WeekSchedule__WeekSchedule_ScheduleGroup");

            builder.HasOne(x => x.ScheduleGroup)
                .WithMany()
                .HasForeignKey(x => x.ScheduleGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__ScheduleGroup__WeekSchedule_ScheduleGroup");
        }

        protected virtual void BuildActivity(EntityTypeBuilder<Activity> builder)
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
                .HasConstraintName("FK_TimePeriod_Activity");

            builder.HasOne(x => x.Subject)
                .WithMany()
                .HasForeignKey("SubjectId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Subject_Activity");

            builder.HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey("RoomId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Room_Activity");

            builder.HasOne(x => x.Lecturer)
                .WithMany()
                .HasForeignKey("LecturerId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Lecturer_Activity");
        }

        protected virtual void BuildSchedule(EntityTypeBuilder<ScheduleGroup> builder)
        {
            builder.ToTable("ScheduleGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));
        }

        protected virtual void BuildDaySchedule(EntityTypeBuilder<DaySchedule> builder)
        {
            builder.ToTable("DayScheduling");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.Property(x => x.DedicatedDate)
                .IsRequired(false)
                .HasConversion(x => DateTimeConversionHelper.ToFileTimeUtc(x), x => DateTimeConversionHelper.FromFileTimeUtc(x));
        }

        protected virtual void BuildWeekSchedule(EntityTypeBuilder<WeekSchedule> builder)
        {
            builder.ToTable("WeekScheduling");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.WeekNumber)
                .IsRequired();
        }
    }
}