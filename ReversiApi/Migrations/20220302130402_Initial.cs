using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations;

[ExcludeFromCodeCoverage]
public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Players",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Color = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Games",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PlayerOneId = table.Column<int>(type: "int", nullable: true),
                PlayerTwoId = table.Column<int>(type: "int", nullable: true),
                CurrentPlayerId = table.Column<int>(type: "int", nullable: true),
                Board = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Games", x => x.Id);
                table.ForeignKey(
                    name: "FK_Games_Players_CurrentPlayerId",
                    column: x => x.CurrentPlayerId,
                    principalTable: "Players",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Games_Players_PlayerOneId",
                    column: x => x.PlayerOneId,
                    principalTable: "Players",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Games_Players_PlayerTwoId",
                    column: x => x.PlayerTwoId,
                    principalTable: "Players",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Games_CurrentPlayerId",
            table: "Games",
            column: "CurrentPlayerId");

        migrationBuilder.CreateIndex(
            name: "IX_Games_PlayerOneId",
            table: "Games",
            column: "PlayerOneId");

        migrationBuilder.CreateIndex(
            name: "IX_Games_PlayerTwoId",
            table: "Games",
            column: "PlayerTwoId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Games");

        migrationBuilder.DropTable(
            name: "Players");
    }
}
