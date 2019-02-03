using B4Interview.DataLayer.Models;
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

        }
    }
}
