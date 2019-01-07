using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace B4Interview.Models
{
    public class DatabaseContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var tag1 = new Tag
            {
                Id = 1,
                ReviewId = 1,
                Name = "OnSite"
            };
            var tag2 = new Tag
            {
                Id = 2,
                ReviewId = 1,
                Name = "Work Life Balance"
            };
            var tag3 = new Tag
            {
                Id = 3,
                ReviewId = 2,
                Name = "Learning"
            };
            var gallery1 = new Gallery
            {
                Id = 1,
                CompanyId = 1,
                Url = "https://upload.wikimedia.org/wikipedia/commons/8/85/Tata_Consultancy_Services_Madhapur_Hyderabad.jpg",
                About = "Tata Consultancy Services campus in Hyderabad, India"
            };
            var gallery2 = new Gallery
            {
                Id = 2,
                CompanyId = 1,
                Url = "https://upload.wikimedia.org/wikipedia/commons/a/a7/TCS-Siruseri-Building.jpg",
                About = "TCS siruseri, Chennai"
            };
            var review1 = new Review
            {
                Id = 1,
                CompanyId = 1,
                Title = "good company to work",
                AutherInfo = "Current Employee - IT Analyst in Kolkata",
                Description = "I have been working at Tata Consultancy Services full-time (More than 5 years)",
                Pros = "1.best medical insurance 2. Lot of opportunities and onsite",
                Cons = "pay hike is less and increment is low",
                Recommend = true,
                Author = "Anonymous",
                CreatedOn = DateTime.Parse("2019/01/04"),
                Rating = 4.5f
            };
            var review2 = new Review
            {
                Id = 2,
                CompanyId = 1,
                Title = "Good place to start with.",
                AutherInfo = "Current Employee - Assistant Systems Engineer in Chennai",
                Description = "I have been working at Tata Consultancy Services full-time (More than a year)",
                Pros = "Learning opportunity is more if you try yourself.",
                Cons = "No proper transparency in management.",
                Recommend = true,
                Author = "Anonymous",
                CreatedOn = DateTime.Parse("2018/01/04"),
                Rating = 3.5f
            };
            var company1 = new Company
            {
                Id = 1,
                About = "Tata Consultancy Services (TCS) helps its clients say farewell to business inefficiencies. The company is a leading global provider of IT, consulting, and outsourcing services, with operations in more than 40 countries. Its offerings include business process outsourcing, data center management, IT and strategic consulting, new product development and engineering, and systems integration. One of its specialties is developing and maintaining customized software for businesses. Most of its clients, in industries ranging from energy to financial services to retail to telecom, are located in North America, Latin America, and Europe. TCS is controlled by textiles and manufacturing conglomerate Tata Group.",
                Headquarter = "Mumbai",
                EmployeeStrength = "400,875",
                Identifier = "Tcs",
                Sector = "IT services, IT consulting",
                Founded = 1968,
                WebSite = "www.tcs.com",
                Name = "Tata Consultancy Services",
                Rating = 0,
                Logo = "https://upload.wikimedia.org/wikipedia/en/b/b1/Tata_Consultancy_Services_Logo.svg"
            };

            modelBuilder.Entity<Company>().HasData(company1);
            modelBuilder.Entity<Gallery>().HasData(gallery1, gallery2);
            modelBuilder.Entity<Review>().HasData(review1, review2);
            modelBuilder.Entity<Tag>().HasData(tag1, tag2, tag3);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string About { get; set; }
        public string EmployeeStrength { get; set; }
        public string WebSite { get; set; }
        public string Headquarter { get; set; }
        public float Rating { get; set; }
        public int Founded { get; set; }
        public string Sector { get; set; }
        public string Identifier { get; set; }
        public string Logo { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Gallery> Images { get; set; }
    }

    public class Review
    {
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AutherInfo { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Recommend { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int ViewsCount { get; set; }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public float Rating { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }

    public class Gallery
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
