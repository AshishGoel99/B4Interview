using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Job> PostedJobs { get; set; }
        public ICollection<JobApplication> Applications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public string Picture { get; set; }
        public virtual JobProfile JobProfile { get; set; }
    }

    public class JobProfile
    {
        public int Id { get; set; }
        public virtual ApplicationUser Candidate { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public string ResumeFileName { get; set; }
        public byte[] Resume { get; set; }
    }
}
