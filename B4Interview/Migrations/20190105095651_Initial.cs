using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B4Interview.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    EmployeeStrength = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    Headquarter = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    Founded = table.Column<int>(nullable: false),
                    Sector = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallery_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    AutherInfo = table.Column<string>(nullable: true),
                    Pros = table.Column<string>(nullable: true),
                    Cons = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Recommend = table.Column<bool>(nullable: false),
                    UpVote = table.Column<int>(nullable: false),
                    DownVote = table.Column<int>(nullable: false),
                    ViewsCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "About", "EmployeeStrength", "Founded", "Headquarter", "Identifier", "Name", "Rating", "Sector", "WebSite" },
                values: new object[] { 1, "Tata Consultancy Services (TCS) helps its clients say farewell to business inefficiencies. The company is a leading global provider of IT, consulting, and outsourcing services, with operations in more than 40 countries. Its offerings include business process outsourcing, data center management, IT and strategic consulting, new product development and engineering, and systems integration. One of its specialties is developing and maintaining customized software for businesses. Most of its clients, in industries ranging from energy to financial services to retail to telecom, are located in North America, Latin America, and Europe. TCS is controlled by textiles and manufacturing conglomerate Tata Group.", "400,875", 1968, "Mumbai", "Tcs", "Tata Consultancy Services", 0f, "IT services, IT consulting", "www.tcs.com" });

            migrationBuilder.InsertData(
                table: "Gallery",
                columns: new[] { "Id", "CompanyId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://en.wikipedia.org/wiki/File:Tata_Consultancy_Services_Madhapur_Hyderabad.jpg" },
                    { 2, 1, "https://en.wikipedia.org/wiki/File:TCS-Siruseri-Building.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AutherInfo", "Author", "CompanyId", "Cons", "CreatedOn", "Description", "DownVote", "Pros", "Rating", "Recommend", "Title", "UpVote", "ViewsCount" },
                values: new object[,]
                {
                    { 1, "Current Employee - IT Analyst in Kolkata", "Anonymous", 1, "pay hike is less and increment is low", new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "I have been working at Tata Consultancy Services full-time (More than 5 years)", 0, "1.best medical insurance 2. Lot of opportunities and onsite", 4.5f, true, "good company to work", 0, 0 },
                    { 2, "Current Employee - Assistant Systems Engineer in Chennai", "Anonymous", 1, "No proper transparency in management.", new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "I have been working at Tata Consultancy Services full-time (More than a year)", 0, "Learning opportunity is more if you try yourself.", 3.5f, true, "Good place to start with.", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "ReviewId" },
                values: new object[] { 1, "OnSite", 1 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "ReviewId" },
                values: new object[] { 2, "Work Life Balance", 1 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name", "ReviewId" },
                values: new object[] { 3, "Learning", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_CompanyId",
                table: "Gallery",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ReviewId",
                table: "Tags",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
