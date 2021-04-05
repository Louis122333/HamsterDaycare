using Microsoft.EntityFrameworkCore.Migrations;

namespace Hamsterdagis.Migrations
{
    public partial class fixcage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HamsterId",
                table: "Cages",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HamsterId",
                table: "Cages");
        }
    }
}
