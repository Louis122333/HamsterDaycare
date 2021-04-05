using Microsoft.EntityFrameworkCore.Migrations;

namespace Hamsterdagis.Migrations
{
    public partial class logrename02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Hamsters_HamsterId",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "ActivityLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_HamsterId",
                table: "ActivityLogs",
                newName: "IX_ActivityLogs_HamsterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs",
                column: "ActivityLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Hamsters_HamsterId",
                table: "ActivityLogs",
                column: "HamsterId",
                principalTable: "Hamsters",
                principalColumn: "HamsterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Hamsters_HamsterId",
                table: "ActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs");

            migrationBuilder.RenameTable(
                name: "ActivityLogs",
                newName: "Logs");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityLogs_HamsterId",
                table: "Logs",
                newName: "IX_Logs_HamsterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "ActivityLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Hamsters_HamsterId",
                table: "Logs",
                column: "HamsterId",
                principalTable: "Hamsters",
                principalColumn: "HamsterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
