using System.Collections.Generic;
using System.Linq;
using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace B4Interview.Pages
{
    public class JobApplicationModel : BaseModel
    {
        public JobApplicationModel(DatabaseContext context) : base(context) { }

        [BindProperty(SupportsGet = true)]
        public int JobId { get; set; }

        public IList<JobApplication> Applications { get; set; }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = UserId;
                Applications = databaseContext.JobApplications
                    .Where(j => j.ApplicantId != userId && j.JobId == JobId).ToList();
            }
        }
    }
}