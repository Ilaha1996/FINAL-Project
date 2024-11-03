using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.AppointmentSystem.DATA.Migrations
{
    /// <inheritdoc />
    public partial class TimeSlotFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_Date_StartTime_EndTime",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimeSlots");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_Date_StartTime",
                table: "TimeSlots",
                columns: new[] { "Date", "StartTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_Date_StartTime",
                table: "TimeSlots");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TimeSlots",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_Date_StartTime_EndTime",
                table: "TimeSlots",
                columns: new[] { "Date", "StartTime", "EndTime" },
                unique: true);
        }
    }
}
