using B4Interview.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyModel : PageModel
    {
        private readonly DatabaseContext databaseContext;

        public CompanyModel(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IList<Company> Companies { get; set; }
        public void OnGet(int index = 0, int count = 10)
        {
            Companies = databaseContext.Companies
                .OrderByDescending(c => c.Rating)
                .Skip(index)
                .Take(count)
                .Include(c => c.Reviews)
                .ToList();
        }
    }
}