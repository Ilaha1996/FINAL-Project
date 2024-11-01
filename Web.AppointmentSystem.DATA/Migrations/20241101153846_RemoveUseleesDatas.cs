using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.AppointmentSystem.DATA.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUseleesDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceImages_ServiceID",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceImages_ServiceID",
                table: "ServiceImages",
                column: "ServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceImages_ServiceID",
                table: "ServiceImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceImages_ServiceID",
                table: "ServiceImages",
                column: "ServiceID",
                unique: true);
        }
    }
}
