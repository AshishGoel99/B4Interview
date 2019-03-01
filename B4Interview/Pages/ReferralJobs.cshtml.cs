using B4Interview.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class ReferralJobModel : BaseModel
    {
        public ReferralJobModel(DatabaseContext context) : base(context) { }

        public IList<Job> Jobs { get; set; }
        public void OnGet(string Company, string Position, string Skill)
        {
            var jobs = databaseContext.Jobs
                    .OrderByDescending(j => j.PostedOn)
                    .Where(j => !j.InActive);

            if (!string.IsNullOrWhiteSpace(Company))
            {
                jobs = jobs.Where(j => j.Company.Identifier == Company);
            }
            else if (!string.IsNullOrWhiteSpace(Position))
            {
                jobs = jobs.Where(j => j.Identifier == Position);
            }
            else if (!string.IsNullOrWhiteSpace(Skill))
            {
                jobs = jobs.Where(j => j.Skills.Any(s => s.Identifier == Skill));
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = UserId;
                jobs = jobs.Where(j => !j.Applications.Any(a => a.ApplicantId == userId) && j.Referrer.Id != userId);
            }

            Jobs = jobs.ToList();
        }
    }
}