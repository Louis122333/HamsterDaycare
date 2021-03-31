using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hamsterdagis.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    CageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.CageId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseArea",
                columns: table => new
                {
                    ExerciseAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseArea", x => x.ExerciseAreaId);
                });

            migrationBuilder.CreateTable(
                name: "Hamsters",
                columns: table => new
                {
                    HamsterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastTimeExercised = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExerciseAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamsters", x => x.HamsterId);
                    table.ForeignKey(
                        name: "FK_Hamsters_ExerciseArea_ExerciseAreaId",
                        column: x => x.ExerciseAreaId,
                        principalTable: "ExerciseArea",
                        principalColumn: "ExerciseAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CageHamster",
                columns: table => new
                {
                    CagesCageId = table.Column<int>(type: "int", nullable: false),
                    HamsterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CageHamster", x => new { x.CagesCageId, x.HamsterId });
                    table.ForeignKey(
                        name: "FK_CageHamster_Cages_CagesCageId",
                        column: x => x.CagesCageId,
                        principalTable: "Cages",
                        principalColumn: "CageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CageHamster_Hamsters_HamsterId",
                        column: x => x.HamsterId,
                        principalTable: "Hamsters",
                        principalColumn: "HamsterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CageHamster_HamsterId",
                table: "CageHamster",
                column: "HamsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseAreaId",
                table: "Hamsters",
                column: "ExerciseAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CageHamster");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "Hamsters");

            migrationBuilder.DropTable(
                name: "ExerciseArea");
        }
    }
}
