using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B4Interview.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Companies_CompanyId",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewRound_Interviews_InterviewId",
                table: "InterviewRound");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Companies_CompanyId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Jobs_JobId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_InterviewRound_InterviewRoundId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Skill_SkillId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Jobs_JobId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Reviews_ReviewId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Interviews_InterviewId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Reviews_ReviewId",
                table: "Vote");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CompanyId", "Description", "Experience", "InActive", "Location", "PostedOn", "ReferrerId", "Title" },
                values: new object[] { 1, 1, "Toolkit API Developer India,NoidaJob DescriptionThis is for an API developer for Delphix DB Lab.", "4-5 years", false, "Noida", new DateTime(2019, 1, 24, 14, 49, 36, 367, DateTimeKind.Local).AddTicks(5191), "102c7bdb-063a-41c8-9d0a-a5168b16699d", "Toolkit API Developer" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Companies_CompanyId",
                table: "Gallery",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewRound_Interviews_InterviewId",
                table: "InterviewRound",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Companies_CompanyId",
                table: "Interviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Jobs_JobId",
                table: "JobApplication",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_InterviewRound_InterviewRoundId",
                table: "Question",
                column: "InterviewRoundId",
                principalTable: "InterviewRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Skill_SkillId",
                table: "Question",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Jobs_JobId",
                table: "Skill",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Reviews_ReviewId",
                table: "Tag",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Interviews_InterviewId",
                table: "Vote",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Reviews_ReviewId",
                table: "Vote",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Companies_CompanyId",
                table: "Gallery");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewRound_Interviews_InterviewId",
                table: "InterviewRound");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Companies_CompanyId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Jobs_JobId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_InterviewRound_InterviewRoundId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Skill_SkillId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Jobs_JobId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Reviews_ReviewId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Interviews_InterviewId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Reviews_ReviewId",
                table: "Vote");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Companies_CompanyId",
                table: "Gallery",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewRound_Interviews_InterviewId",
                table: "InterviewRound",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Companies_CompanyId",
                table: "Interviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Jobs_JobId",
                table: "JobApplication",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_InterviewRound_InterviewRoundId",
                table: "Question",
                column: "InterviewRoundId",
                principalTable: "InterviewRound",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Skill_SkillId",
                table: "Question",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Jobs_JobId",
                table: "Skill",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Reviews_ReviewId",
                table: "Tag",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Interviews_InterviewId",
                table: "Vote",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Reviews_ReviewId",
                table: "Vote",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
