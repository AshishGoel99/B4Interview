using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Company
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string About { get; set; }
        public string EmployeeStrength { get; set; }
        public string WebSite { get; set; }
        public string Headquarter { get; set; }
        public float Rating { get; set; }//this is to be updated on every review insert
        public decimal ReviewsCount { get; set; }//this is to be updated on every review insert
        public int Founded { get; set; }
        public string Sector { get; set; }
        public string Identifier { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Gallery> Images { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
    }
}
