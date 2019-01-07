using B4Interview.Models;
using Microsoft.AspNetCore.Mvc;
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
        public int RemainingPagesCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Index { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        public void OnGet()
        {
            if (PageSize == 0)
                PageSize = 12;

            var next = databaseContext.Companies
                .OrderByDescending(c => c.Rating)
                .Skip(Index * PageSize);

            var remaining = next.Count() - PageSize;
            RemainingPagesCount = remaining / PageSize;
            if (RemainingPagesCount * PageSize < remaining)
                RemainingPagesCount++;

            Companies = next
                .Take(PageSize)
                .Include(c => c.Reviews)
                .ToList();
        }
    }
}