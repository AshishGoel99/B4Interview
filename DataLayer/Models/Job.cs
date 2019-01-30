using System;
using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
        public DateTime PostedOn { get; set; }
        public virtual ApplicationUser Referrer { get; set; }
        public string ReferrerId { get; set; }
        public virtual ICollection<JobApplication> Applications { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public bool InActive { get; set; }
    }
}
