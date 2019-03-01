using Microsoft.EntityFrameworkCore.Migrations;

namespace B4Interview.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Interviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Interviews");
        }
    }
}
