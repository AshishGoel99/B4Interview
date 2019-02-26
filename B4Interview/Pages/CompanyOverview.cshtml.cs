using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyOverviewModel : BaseModel
    {
        public CompanyOverviewModel(DatabaseContext context) : base(context) { }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public Company Company { get; set; }
        public void OnGet()
        {
            Company = databaseContext.Companies.Where(c => c.Identifier == Search || c.Name == Search)
                .First();
        }


        public JsonResult OnGetNamesAndPosition(string query, string type)
        {
            if (type.ToUpper() == "JOBS")
            {
                return new JsonResult(
                databaseContext.Jobs
                .Where(c => c.Title.ToUpper().Contains(query.ToUpper()) || c.Skills.Any(s => s.Name.ToUpper().Contains(query.ToUpper())))
                .Select(c => c.Title)
                );
            }
            else if (type.ToUpper() == "INTERVIEWS")
            {
                var companies = GetCompanies(query);
                var skills = GetSkills(query);

                var positions = databaseContext.Interviews
                .Where(c => c.Title.ToUpper().Contains(query.ToUpper()))
                .Select(c => c.Title)
                .Distinct();

                return new JsonResult(companies.Concat(positions).Concat(skills));
            }
            else
            {
                return OnGetNames(query);
            }
        }

        public JsonResult OnGetNames(string query)
        {
            return new JsonResult(GetCompanies(query));
        }

        public JsonResult OnGetSkills(string query)
        {
            return new JsonResult(GetSkills(query));
        }

        private IQueryable<string> GetCompanies(string query)
        {
            return databaseContext.Companies
                .Where(c => c.Name.ToUpper().Contains(query.ToUpper())
                            ||
                            c.Identifier.ToUpper().Contains(query.ToUpper()))
                .Select(c => c.Name);
        }

        private IQueryable<string> GetSkills(string query)
        {
            return databaseContext.Skills
            .Where(c => c.Name.ToUpper().Contains(query.ToUpper()))
            .Select(c => c.Name);
        }
    }
}