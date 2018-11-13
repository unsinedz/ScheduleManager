﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleManager.Data;

namespace ScheduleManager.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.ActivityDaySchedule", b =>
                {
                    b.Property<Guid>("ActivityId");

                    b.Property<Guid>("DayScheduleId");

                    b.HasKey("ActivityId", "DayScheduleId");

                    b.HasIndex("DayScheduleId");

                    b.ToTable("Activity_DaySchedule");
                });

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.DayScheduleWeekSchedule", b =>
                {
                    b.Property<Guid>("DayScheduleId");

                    b.Property<Guid>("WeekScheduleId");

                    b.HasKey("DayScheduleId", "WeekScheduleId");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("DaySchedule_WeekSchedule");
                });

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.WeekScheduleSchedule", b =>
                {
                    b.Property<Guid>("WeekScheduleId");

                    b.Property<Guid>("ScheduleGroupId");

                    b.HasKey("WeekScheduleId", "ScheduleGroupId");

                    b.HasIndex("ScheduleGroupId");

                    b.ToTable("WeekSchedule_ScheduleGroup");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActivityId");

                    b.Property<int>("AttendeeType");

                    b.Property<Guid?>("CourseId");

                    b.Property<Guid?>("FK_Faculty_Attendee");

                    b.Property<Guid?>("FacultyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FK_Faculty_Attendee");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.TimePeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("End");

                    b.Property<long>("Start");

                    b.HasKey("Id");

                    b.ToTable("TimePeriods");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Faculties.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FacultyId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Faculties.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Faculties.Lecturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DayScheduleId");

                    b.Property<Guid?>("LecturerId");

                    b.Property<Guid?>("RoomId");

                    b.Property<Guid?>("SubjectId");

                    b.Property<Guid?>("TimePeriodId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DayScheduleId");

                    b.HasIndex("LecturerId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TimePeriodId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.DaySchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeek");

                    b.Property<long?>("DedicatedDate");

                    b.Property<Guid?>("WeekScheduleId");

                    b.HasKey("Id");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("DayScheduling");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.ScheduleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("ScheduleGroups");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.WeekSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ScheduleGroupId");

                    b.Property<int>("WeekNumber");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleGroupId");

                    b.ToTable("WeekScheduling");
                });

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.ActivityDaySchedule", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .HasConstraintName("FK__Activity__Activity_DaySchedule")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScheduleManager.Domain.Scheduling.DaySchedule", "DaySchedule")
                        .WithMany()
                        .HasForeignKey("DayScheduleId")
                        .HasConstraintName("FK__DaySchedule__Activity_DaySchedule")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.DayScheduleWeekSchedule", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.DaySchedule", "DaySchedule")
                        .WithMany()
                        .HasForeignKey("DayScheduleId")
                        .HasConstraintName("FK__DaySchedule__DaySchedule_WeekSchedule")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScheduleManager.Domain.Scheduling.WeekSchedule", "WeekSchedule")
                        .WithMany()
                        .HasForeignKey("WeekScheduleId")
                        .HasConstraintName("FK__WeekSchedule__DaySchedule_WeekSchedule")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScheduleManager.Data.Scheduling.Relations.WeekScheduleSchedule", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.ScheduleGroup", "ScheduleGroup")
                        .WithMany()
                        .HasForeignKey("ScheduleGroupId")
                        .HasConstraintName("FK__ScheduleGroup__WeekSchedule_ScheduleGroup")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScheduleManager.Domain.Scheduling.WeekSchedule", "WeekSchedule")
                        .WithMany()
                        .HasForeignKey("WeekScheduleId")
                        .HasConstraintName("FK__WeekSchedule__WeekSchedule_ScheduleGroup")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ScheduleManager.Domain.Common.Attendee", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.Activity")
                        .WithMany("Attendees")
                        .HasForeignKey("ActivityId");

                    b.HasOne("ScheduleManager.Domain.Common.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Course_Attendee")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ScheduleManager.Domain.Faculties.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FK_Faculty_Attendee")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ScheduleManager.Domain.Faculties.Department", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Faculties.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .HasConstraintName("FK_Faculty_Department")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ScheduleManager.Domain.Faculties.Lecturer", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Faculties.Department", "Department")
                        .WithMany("Lecturers")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Department_Lecturer")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.Activity", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.DaySchedule")
                        .WithMany("Activities")
                        .HasForeignKey("DayScheduleId");

                    b.HasOne("ScheduleManager.Domain.Faculties.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .HasConstraintName("FK_Lecturer_Activity")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ScheduleManager.Domain.Common.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK_Room_Activity")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ScheduleManager.Domain.Common.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK_Subject_Activity")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ScheduleManager.Domain.Common.TimePeriod", "TimePeriod")
                        .WithMany()
                        .HasForeignKey("TimePeriodId")
                        .HasConstraintName("FK_TimePeriod_Activity")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.DaySchedule", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.WeekSchedule")
                        .WithMany("Days")
                        .HasForeignKey("WeekScheduleId");
                });

            modelBuilder.Entity("ScheduleManager.Domain.Scheduling.WeekSchedule", b =>
                {
                    b.HasOne("ScheduleManager.Domain.Scheduling.ScheduleGroup")
                        .WithMany("Weeks")
                        .HasForeignKey("ScheduleGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
