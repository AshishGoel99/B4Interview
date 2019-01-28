using System.Linq;
using B4Interview.DataLayer.Models;

namespace B4Interview.Pages
{
    public class CandidateProfileModel : BaseModel
    {
        public CandidateProfileModel(DatabaseContext context) : base(context) { }

        public ApplicationUser Candidate { get; set; }

        public void OnGet(string id)
        {
            Candidate = databaseContext.Users.First(u => u.Id == id);
        }
    }
}