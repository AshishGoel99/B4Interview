﻿using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace B4Interview.Pages
{
    public class JobDetailModel : PageModel
    {
        private readonly DatabaseContext context;

        public JobDetailModel(DatabaseContext context)
        {
            this.context = context;
        }

        public Job Job { get; set; }

        [BindProperty(SupportsGet = true)]
        public int JobId { get; set; }

        public void OnGet()
        {
            Job = context.Jobs
                .Include(j => j.Skills)
                .Include(j => j.Referrer)
                .Include(j => j.Company)
                .First(j => j.Id == JobId);
        }

        public IActionResult OnGetJobApply()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            context.Jobs
                .Include(j => j.Applications)
                .First(j => j.Id == JobId)
                .Applications.Add(new JobApplication
                {
                    JobId = JobId,
                    ApplicantId = userId
                });
            context.SaveChanges();

            return new RedirectToPageResult("ReferralJob", new { response = "Job Applied successfully" });
        }
    }
}