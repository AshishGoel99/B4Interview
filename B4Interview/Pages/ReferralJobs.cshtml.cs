using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace B4Interview.Pages
{
    public class ReferralJobModel : PageModel
    {
        private readonly DatabaseContext context;

        public ReferralJobModel(DatabaseContext context)
        {
            this.context = context;
        }

        public IList<Job> Jobs { get; set; }
        public void OnGet(string search)
        {
            var jobs = context.Jobs
                    .OrderByDescending(j => j.PostedOn)
                    .Include(j => j.Company)
                    .Include(j => j.Skills)
                    .Include(j => j.Referrer)
                    .Where(j => !j.InActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                jobs = jobs.Where(j => j.Title == search || j.Skills.Any(s => s.Name == search));
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                jobs = jobs.Where(j => !j.Applications.Any(a => a.ApplicantId == userId));
            }

            Jobs = jobs.ToList();
        }
    }
}