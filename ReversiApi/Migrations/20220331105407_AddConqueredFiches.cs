using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations;

public partial class AddConqueredFiches : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ConqueredBlackFiches",
            table: "Games",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "ConqueredWhiteFiches",
            table: "Games",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ConqueredBlackFiches",
            table: "Games");

        migrationBuilder.DropColumn(
            name: "ConqueredWhiteFiches",
            table: "Games");
    }
}
