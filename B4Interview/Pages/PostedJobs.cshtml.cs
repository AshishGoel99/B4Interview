using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace B4Interview.Pages
{
    public class PostedJobsModel : PageModel
    {
        private readonly DatabaseContext context;

        public PostedJobsModel(DatabaseContext context)
        {
            this.context = context;
        }

        public IList<Job> Jobs { get; set; }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Jobs = context.Jobs
                    .OrderByDescending(j => j.PostedOn)
                    .Include(j => j.Company)
                    .Include(j => j.Applications)
                    .Include(j => j.Skills)
                    .Include(j => j.Referrer)
                    .Where(j =>
                    !j.InActive &&
                    j.Referrer.Id == userId
                ).ToList();
            }
        }
    }
}