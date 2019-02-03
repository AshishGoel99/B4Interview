using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace B4Interview.Pages
{
    public class JobDetailModel : BaseModel
    {
        public JobDetailModel(DatabaseContext context) : base(context) { }

        public Job Job { get; set; }
        public ApplicationUser Candidate { get; set; }
        //public bool ShouldGiveReview { get; set; }

        [BindProperty(SupportsGet = true)]
        public int JobId { get; set; }

        public void OnGet()
        {
            Job = databaseContext.Jobs
                .First(j => j.Id == JobId);

            if (User.Identity.IsAuthenticated)
            {
                var userId = UserId;
                Candidate = databaseContext.Users
                    .Single(u => u.Id == userId);
            }
        }

        public IActionResult OnGetJobApply()
        {
            var userId = UserId;

            databaseContext.Jobs
                .First(j => j.Id == JobId)
                .Applications.Add(new JobApplication
                {
                    JobId = JobId,
                    ApplicantId = userId
                });
            databaseContext.SaveChanges();

            //Send Mail to the referrer

            StatusMessage = "Job Applied successfully";

            var user = databaseContext.Users.Single(u => u.Id == userId);
            if (!user.Fresher && user.Interviews.Count == 0)
                return new RedirectToPageResult("NewInterview");

            return new RedirectToPageResult("ReferralJobs");
        }
    }
}