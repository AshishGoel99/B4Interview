using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyModel : BaseModel
    {
        public CompanyModel(DatabaseContext context) : base(context) { }

        public IList<Company> Companies { get; set; }
        public int RemainingPagesCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Index { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        public void OnGet()
        {
            if (PageSize == 0)//this is default
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