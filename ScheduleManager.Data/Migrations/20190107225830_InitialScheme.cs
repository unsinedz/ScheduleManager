using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleManager.Data.Migrations
{
    public partial class InitialScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Start = table.Column<long>(nullable: false),
                    End = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    FacultyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculty_Department",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WeekScheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WeekNumber = table.Column<int>(nullable: true),
                    ScheduleGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekScheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekScheduling_ScheduleGroups_ScheduleGroupId",
                        column: x => x.ScheduleGroupId,
                        principalTable: "ScheduleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Lecturer",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DayScheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DedicatedDate = table.Column<long>(nullable: true),
                    DayOfWeek = table.Column<int>(nullable: false),
                    WeekScheduleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayScheduling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayScheduling_WeekScheduling_WeekScheduleId",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeekSchedule_ScheduleGroup",
                columns: table => new
                {
                    WeekScheduleId = table.Column<Guid>(nullable: false),
                    ScheduleGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekSchedule_ScheduleGroup", x => new { x.WeekScheduleId, x.ScheduleGroupId });
                    table.ForeignKey(
                        name: "FK__ScheduleGroup__WeekSchedule_ScheduleGroup",
                        column: x => x.ScheduleGroupId,
                        principalTable: "ScheduleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__WeekSchedule__WeekSchedule_ScheduleGroup",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimePeriodId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    RoomId = table.Column<Guid>(nullable: true),
                    SubjectId = table.Column<Guid>(nullable: true),
                    LecturerId = table.Column<Guid>(nullable: true),
                    DayScheduleId = table.Column<Guid>(nullable: true),
                    DayScheduleId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaySchedule_Activity",
                        column: x => x.DayScheduleId,
                        principalTable: "DayScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Activities_DayScheduling_DayScheduleId1",
                        column: x => x.DayScheduleId1,
                        principalTable: "DayScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturer_Activity",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Room_Activity",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Subject_Activity",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimePeriod_Activity",
                        column: x => x.TimePeriodId,
                        principalTable: "TimePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DaySchedule_WeekSchedule",
                columns: table => new
                {
                    DayScheduleId = table.Column<Guid>(nullable: false),
                    WeekScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedule_WeekSchedule", x => new { x.DayScheduleId, x.WeekScheduleId });
                    table.ForeignKey(
                        name: "FK__DaySchedule__DaySchedule_WeekSchedule",
                        column: x => x.DayScheduleId,
                        principalTable: "DayScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__WeekSchedule__DaySchedule_WeekSchedule",
                        column: x => x.WeekScheduleId,
                        principalTable: "WeekScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity_DaySchedule",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false),
                    DayScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity_DaySchedule", x => new { x.ActivityId, x.DayScheduleId });
                    table.ForeignKey(
                        name: "FK__Activity__Activity_DaySchedule",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__DaySchedule__Activity_DaySchedule",
                        column: x => x.DayScheduleId,
                        principalTable: "DayScheduling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttendeeType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CourseId = table.Column<Guid>(nullable: true),
                    FacultyId = table.Column<Guid>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Attendee",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Faculty_Attendee",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Activity_Attendee",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false),
                    AttendeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity_Attendee", x => new { x.ActivityId, x.AttendeeId });
                    table.ForeignKey(
                        name: "FK__Activity__Activity_Attendee",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Attendee__Activity_Attendee",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayScheduleId",
                table: "Activities",
                column: "DayScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayScheduleId1",
                table: "Activities",
                column: "DayScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_LecturerId",
                table: "Activities",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_RoomId",
                table: "Activities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TimePeriodId",
                table: "Activities",
                column: "TimePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_Attendee_AttendeeId",
                table: "Activity_Attendee",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DaySchedule_DayScheduleId",
                table: "Activity_DaySchedule",
                column: "DayScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_ActivityId",
                table: "Attendees",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_CourseId",
                table: "Attendees",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_FacultyId",
                table: "Attendees",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedule_WeekSchedule_WeekScheduleId",
                table: "DaySchedule_WeekSchedule",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DayScheduling_WeekScheduleId",
                table: "DayScheduling",
                column: "WeekScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId",
                table: "Lecturers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekSchedule_ScheduleGroup_ScheduleGroupId",
                table: "WeekSchedule_ScheduleGroup",
                column: "ScheduleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekScheduling_ScheduleGroupId",
                table: "WeekScheduling",
                column: "ScheduleGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity_Attendee");

            migrationBuilder.DropTable(
                name: "Activity_DaySchedule");

            migrationBuilder.DropTable(
                name: "DaySchedule_WeekSchedule");

            migrationBuilder.DropTable(
                name: "WeekSchedule_ScheduleGroup");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "DayScheduling");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropTable(
                name: "WeekScheduling");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ScheduleGroups");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
