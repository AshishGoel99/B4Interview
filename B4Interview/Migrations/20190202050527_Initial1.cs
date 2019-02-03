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
                columns: new[] { "Id", "CompanyId", "Description", "Experience", "InActive", "Location", "PostedOn", "ReferrerId", "Title" },
                values: new object[] { 1, 1, "Toolkit API Developer India,NoidaJob DescriptionThis is for an API developer for Delphix DB Lab.", "4-5 years", false, "Noida", new DateTime(2019, 2, 2, 10, 35, 26, 63, DateTimeKind.Local).AddTicks(1804), "aa19f5e5-c955-461b-94a5-73ad2ffc9d47", "Toolkit API Developer" });
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
