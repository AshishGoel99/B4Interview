using Microsoft.EntityFrameworkCore.Migrations;

namespace B4Interview.Migrations
{
    public partial class Intial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companies",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Logo",
                value: "https://upload.wikimedia.org/wikipedia/en/b/b1/Tata_Consultancy_Services_Logo.svg");

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://upload.wikimedia.org/wikipedia/commons/8/85/Tata_Consultancy_Services_Madhapur_Hyderabad.jpg");

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://upload.wikimedia.org/wikipedia/commons/a/a7/TCS-Siruseri-Building.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://en.wikipedia.org/wiki/File:Tata_Consultancy_Services_Madhapur_Hyderabad.jpg");

            migrationBuilder.UpdateData(
                table: "Gallery",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://en.wikipedia.org/wiki/File:TCS-Siruseri-Building.jpg");
        }
    }
}
