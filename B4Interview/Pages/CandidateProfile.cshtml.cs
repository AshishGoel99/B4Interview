using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
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
                .First(u => u.Id == id);
        }

        public IActionResult OnGetDownloadResume(string candidateId)
        {
            var candidate = databaseContext.Users.Single(c => c.Id == candidateId);
            return File(candidate.Resume, GetContentType(candidate.ResumeFileName), candidate.ResumeFileName);
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
            };
        }
    }
}