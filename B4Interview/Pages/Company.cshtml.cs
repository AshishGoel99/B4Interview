using B4Interview.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyModel : BaseModel
    {
        public CompanyModel(DatabaseContext context) : base(context) { }

        public IList<Company> Companies { get; set; }

        public void OnGet()
        {
            var data = databaseContext.Companies
                .OrderByDescending(c => c.Reviews.Count());

            Companies = GetPagedData(data).ToList();
        }
    }
}