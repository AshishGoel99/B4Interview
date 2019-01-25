using System.Linq;
using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B4Interview.Pages
{
    public class CandidateProfileModel : PageModel
    {
        private readonly DatabaseContext context;

        public CandidateProfileModel(DatabaseContext context)
        {
            this.context = context;
        }

        public ApplicationUser Candidate { get; set; }

        public void OnGet(string id)
        {
            Candidate = context.Users.First(u => u.Id == id);
        }
    }
}