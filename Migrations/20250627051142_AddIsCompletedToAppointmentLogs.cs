using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Module.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompletedToAppointmentLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "AppointmentLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "AppointmentLogs");
        }
    }
}
