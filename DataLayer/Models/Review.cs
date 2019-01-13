using System;
using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AutherInfo { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Recommend { get; set; }
        public decimal UpVote { get; set; }
        public decimal DownVote { get; set; }
        public decimal ViewsCount { get; set; }
        public float Rating { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
