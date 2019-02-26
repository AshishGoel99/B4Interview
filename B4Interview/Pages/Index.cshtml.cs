using B4Interview.DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class IndexModel : BaseModel
    {
        public IndexModel(DatabaseContext context) : base(context)
        { }


        public IList<Job> RecentReferralJobs { get; set; }

        public void OnGet()
        {
            var jobs = databaseContext.Jobs.Where(j => !j.InActive);

            var userId = UserId;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                jobs = jobs.Where(j => j.ReferrerId != userId);
            }

            RecentReferralJobs = jobs.OrderByDescending(j => j.PostedOn).Take(5).ToList();


            //addSomeData();
        }

        private void addSomeData()
        {
            var data = "";
            var companies = JsonConvert.DeserializeObject<IEnumerable<comp>>(data);

            foreach (var company in companies)
            {
                databaseContext.Companies.Add(new Company
                {
                    About = company.Description,
                    EmployeeStrength = company.Size,
                    Founded = Convert.ToInt32(company.Founded),
                    Name = company.CompanyName,
                    Headquarter = company.Headquarters,
                    Sector = company.Industry,
                    WebSite = company.WebSite
                });
            }

            databaseContext.SaveChanges();
        }

        private class comp
        {
            public string CompanyName { get; set; }
            public string Founded { get; set; }
            public string Size { get; set; }
            public string Description { get; set; }
            public string WebSite { get; set; }
            public string Headquarters { get; set; }
            public string Industry { get; set; }
        }
    }
}
