﻿// <auto-generated />
using System;
using B4Interview;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B4Interview.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("B4Interview.DataLayer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("EmployerId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Picture");

                    b.Property<byte[]>("Resume");

                    b.Property<string>("ResumeFileName");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<string>("EmployeeStrength");

                    b.Property<int>("Founded");

                    b.Property<string>("Headquarter");

                    b.Property<string>("Identifier");

                    b.Property<string>("Logo");

                    b.Property<string>("Name");

                    b.Property<float>("Rating");

                    b.Property<decimal>("ReviewsCount");

                    b.Property<string>("Sector");

                    b.Property<string>("WebSite");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            About = "Tata Consultancy Services (TCS) helps its clients say farewell to business inefficiencies. The company is a leading global provider of IT, consulting, and outsourcing services, with operations in more than 40 countries. Its offerings include business process outsourcing, data center management, IT and strategic consulting, new product development and engineering, and systems integration. One of its specialties is developing and maintaining customized software for businesses. Most of its clients, in industries ranging from energy to financial services to retail to telecom, are located in North America, Latin America, and Europe. TCS is controlled by textiles and manufacturing conglomerate Tata Group.",
                            EmployeeStrength = "400,875",
                            Founded = 1968,
                            Headquarter = "Mumbai",
                            Identifier = "Tcs",
                            Logo = "https://upload.wikimedia.org/wikipedia/en/b/b1/Tata_Consultancy_Services_Logo.svg",
                            Name = "Tata Consultancy Services",
                            Rating = 0f,
                            ReviewsCount = 0m,
                            Sector = "IT services, IT consulting",
                            WebSite = "www.tcs.com"
                        });
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Gallery");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            About = "Tata Consultancy Services campus in Hyderabad, India",
                            CompanyId = 1,
                            Url = "https://upload.wikimedia.org/wikipedia/commons/8/85/Tata_Consultancy_Services_Madhapur_Hyderabad.jpg"
                        },
                        new
                        {
                            Id = 2,
                            About = "TCS siruseri, Chennai",
                            CompanyId = 1,
                            Url = "https://upload.wikimedia.org/wikipedia/commons/a/a7/TCS-Siruseri-Building.jpg"
                        });
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Interview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<short>("Difficulty");

                    b.Property<decimal>("DownVote");

                    b.Property<string>("Experience");

                    b.Property<string>("Location");

                    b.Property<DateTime>("PostedOn");

                    b.Property<short>("Source");

                    b.Property<string>("Title");

                    b.Property<decimal>("UpVote");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.InterviewRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detail");

                    b.Property<int>("InterviewId");

                    b.Property<int>("RoundType");

                    b.HasKey("Id");

                    b.HasIndex("InterviewId");

                    b.ToTable("InterviewRound");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<string>("Experience");

                    b.Property<bool>("InActive");

                    b.Property<string>("Location");

                    b.Property<DateTime>("PostedOn");

                    b.Property<string>("ReferrerId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ReferrerId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            Description = "Toolkit API Developer India,NoidaJob DescriptionThis is for an API developer for Delphix DB Lab.",
                            Experience = "4-5 years",
                            InActive = false,
                            Location = "Noida",
                            PostedOn = new DateTime(2019, 1, 27, 18, 56, 27, 699, DateTimeKind.Local).AddTicks(4451),
                            ReferrerId = "0f5d3de3-3daa-487b-a675-4468a7ab058f",
                            Title = "Toolkit API Developer"
                        });
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.JobApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicantId");

                    b.Property<int>("JobId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("JobId");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detail");

                    b.Property<int>("InterviewRoundId");

                    b.Property<int>("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("InterviewRoundId");

                    b.HasIndex("SkillId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Anonymous");

                    b.Property<string>("AutherInfo");

                    b.Property<string>("AuthorId");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Cons");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<decimal>("DownVote");

                    b.Property<string>("Pros");

                    b.Property<float>("Rating");

                    b.Property<bool>("Recommend");

                    b.Property<string>("Title");

                    b.Property<decimal>("UpVote");

                    b.Property<decimal>("ViewsCount");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Anonymous = false,
                            AutherInfo = "Current Employee - IT Analyst in Kolkata",
                            CompanyId = 1,
                            Cons = "pay hike is less and increment is low",
                            CreatedOn = new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "I have been working at Tata Consultancy Services full-time (More than 5 years)",
                            DownVote = 0m,
                            Pros = "1.best medical insurance 2. Lot of opportunities and onsite",
                            Rating = 4.5f,
                            Recommend = true,
                            Title = "good company to work",
                            UpVote = 0m,
                            ViewsCount = 0m
                        },
                        new
                        {
                            Id = 2,
                            Anonymous = false,
                            AutherInfo = "Current Employee - Assistant Systems Engineer in Chennai",
                            CompanyId = 1,
                            Cons = "No proper transparency in management.",
                            CreatedOn = new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "I have been working at Tata Consultancy Services full-time (More than a year)",
                            DownVote = 0m,
                            Pros = "Learning opportunity is more if you try yourself.",
                            Rating = 3.5f,
                            Recommend = true,
                            Title = "Good place to start with.",
                            UpVote = 0m,
                            ViewsCount = 0m
                        });
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobId");

                    b.Property<string>("Name");

                    b.Property<int>("QuestionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ReviewId");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "OnSite",
                            ReviewId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Work Life Balance",
                            ReviewId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Learning",
                            ReviewId = 2
                        });
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InterviewId");

                    b.Property<int>("ReviewId");

                    b.Property<bool>("UpVote");

                    b.Property<string>("VoterId");

                    b.HasKey("Id");

                    b.HasIndex("InterviewId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("VoterId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.ApplicationUser", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Company", "Employer")
                        .WithMany("Employees")
                        .HasForeignKey("EmployerId");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Gallery", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Company", "Company")
                        .WithMany("Images")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Interview", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Company", "Company")
                        .WithMany("Interviews")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.InterviewRound", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Interview", "Interview")
                        .WithMany("Rounds")
                        .HasForeignKey("InterviewId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Job", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser", "Referrer")
                        .WithMany("PostedJobs")
                        .HasForeignKey("ReferrerId");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.JobApplication", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId");

                    b.HasOne("B4Interview.DataLayer.Models.Job", "Job")
                        .WithMany("Applications")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Question", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.InterviewRound", "InterviewRound")
                        .WithMany("Questions")
                        .HasForeignKey("InterviewRoundId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.Skill", "Skill")
                        .WithMany("Question")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Review", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("AuthorId");

                    b.HasOne("B4Interview.DataLayer.Models.Company", "Company")
                        .WithMany("Reviews")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Skill", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Job", "Job")
                        .WithMany("Skills")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser", "User")
                        .WithMany("Skills")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Tag", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Review", "Review")
                        .WithMany("Tags")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("B4Interview.DataLayer.Models.Vote", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.Interview", "Interview")
                        .WithMany("Votes")
                        .HasForeignKey("InterviewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.Review", "Review")
                        .WithMany("Votes")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser", "Voter")
                        .WithMany("Votes")
                        .HasForeignKey("VoterId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("B4Interview.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
