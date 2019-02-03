using System.Collections.Generic;
using System.Linq;
using B4Interview.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B4Interview.Pages
{
    public class PostedJobsModel : BaseModel
    {
        public PostedJobsModel(DatabaseContext context) : base(context) { }

        public IList<Job> Jobs { get; set; }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = UserId;

                Jobs = databaseContext.Jobs
                    .OrderByDescending(j => j.PostedOn)
                    .Where(j =>
                    !j.InActive &&
                    j.Referrer.Id == userId
                ).ToList();
            }
        }
    }
}