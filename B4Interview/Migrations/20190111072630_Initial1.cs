using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B4Interview.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CompanyId", "Description", "Experience", "Location", "PostedOn", "ReferrerId", "Title" },
                values: new object[] { 1, 1, "Toolkit API Developer India,NoidaJob DescriptionThis is for an API developer for Delphix DB Lab.", "4-5 years", "Noida", new DateTime(2019, 1, 11, 12, 56, 29, 815, DateTimeKind.Local).AddTicks(1546), "70efc0fb-a859-4225-9e8a-96b9bfd699ad", "Toolkit API Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
