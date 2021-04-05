using Microsoft.EntityFrameworkCore.Migrations;

namespace Hamsterdagis.Migrations
{
    public partial class logrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "Logs",
                newName: "ActivityLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityLogId",
                table: "Logs",
                newName: "LogId");
        }
    }
}
