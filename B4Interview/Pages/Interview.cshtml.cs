using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class InterviewModel : PageModel
    {
        private readonly DatabaseContext context;

        public InterviewModel(DatabaseContext context)
        {
            this.context = context;
        }

        public IList<Interview> Interviews { get; set; }

        public void OnGet(string company, string skill)
        {
            var interviews = context.Interviews
                .OrderByDescending(i => i.PostedOn);

            if (!string.IsNullOrWhiteSpace(company))
            {
                Interviews = interviews
                    .Where(i => i.Company.Name == company || i.Company.Identifier == company)
                    .ToList();
            }
            else if (!string.IsNullOrWhiteSpace(skill))
            {
                Interviews = interviews
                    .Where(i => i.Skills.Any(s => s.Name.ToUpper() == skill.ToUpper()))
                    .ToList();
            }
            else
            {
                Interviews = interviews.ToList();
            }
        }
    }
}