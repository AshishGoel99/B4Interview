using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Job> PostedJobs { get; set; }
        public virtual ICollection<JobApplication> Applications { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

        public string Picture { get; set; }
        public string ResumeFileName { get; set; }
        public byte[] Resume { get; set; }
        public virtual Company Employer { get; set; }
        public int? EmployerId { get; set; }
        public bool Fresher { get; set; }
    }
}
