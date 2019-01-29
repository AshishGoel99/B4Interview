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
                .Include(j => j.Skills)
                .Include(j => j.Referrer)
                .Include(j => j.Company)
                .First(j => j.Id == JobId);

            if (User.Identity.IsAuthenticated)
            {
                var userId = UserId;
                Candidate = databaseContext.Users
                    .Include(u => u.Employer)
                    .Include(u => u.Reviews)
                    .Single(u => u.Id == userId);
                //var ReviewGiven = databaseContext.Reviews.Any(r => r.Author.Id == userId);
                //if (!ReviewGiven)
                //{
                //    ShouldGiveReview = !databaseContext.Users.Any(u => u.Id == userId && u.Fresher);
                //}
            }
        }

        public IActionResult OnGetJobApply()
        {
            var userId = UserId;

            databaseContext.Jobs
                .Include(j => j.Applications)
                .First(j => j.Id == JobId)
                .Applications.Add(new JobApplication
                {
                    JobId = JobId,
                    ApplicantId = userId
                });
            databaseContext.SaveChanges();

            //Send Mail to the referrer


            return new RedirectToPageResult("ReferralJob", new { response = "Job Applied successfully" });
        }
    }
}