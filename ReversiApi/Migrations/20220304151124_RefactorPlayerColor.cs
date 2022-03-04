using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReversiApi.Migrations;

[ExcludeFromCodeCoverage]
public partial class RefactorPlayerColor : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Color",
            table: "Players");

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Players",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "Players");

        migrationBuilder.AddColumn<int>(
            name: "Color",
            table: "Players",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }
}