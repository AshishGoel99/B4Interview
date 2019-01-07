using B4Interview.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

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
        public void OnGet()
        {
            Jobs = context.Jobs
                .OrderByDescending(j => j.PostedOn).ToList();
        }
    }
}