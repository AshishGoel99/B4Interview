using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B4Interview.Pages
{
    public class JobApplicationModel : PageModel
    {
        private readonly DatabaseContext context;

        public JobApplicationModel(DatabaseContext context)
        {
            this.context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int JobId { get; set; }

        public IList<JobApplication> Applications { get; set; }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Applications = context.JobApplications
                    .Where(j => j.ApplicantId != userId && j.JobId == JobId).ToList();
            }
        }
    }
}