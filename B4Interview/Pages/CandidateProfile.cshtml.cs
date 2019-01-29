using B4Interview.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace B4Interview.Pages
{
    public class CandidateProfileModel : BaseModel
    {
        public CandidateProfileModel(DatabaseContext context) : base(context) { }

        public ApplicationUser Candidate { get; set; }

        public void OnGet(string id)
        {
            Candidate = databaseContext.Users
                .Include(c => c.Skills)
                .First(u => u.Id == id);
        }
    }
}