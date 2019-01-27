using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyOverviewModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CompanyOverviewModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public Company Company { get; set; }
        public void OnGet()
        {
            Company = _context.Companies.Where(c => c.Identifier == Search || c.Name == Search)
                .Include(c => c.Images)
                .First();
        }


        public JsonResult OnGetNamesAndPosition(string query, string type)
        {
            if (type.ToUpper() == "JOBS")
            {
                return new JsonResult(
                _context.Jobs
                .Where(c => c.Title.Contains(query) || c.Skills.Any(s => s.Name == query))
                .Select(c => c.Title)
                );
            }
            else
            {
                return OnGetNames(query);
                //return new JsonResult(
                //_context.Companies
                //.Where(c => c.Name.Contains(query) || c.Identifier.Contains(query))
                //.Select(c => c.Name)
                //);
            }
        }

        public JsonResult OnGetNames(string query)
        {
            return new JsonResult(
            _context.Companies
            .Where(c => c.Name.Contains(query) || c.Identifier.Contains(query))
            .Select(c => c.Name)
            );
        }
    }
}