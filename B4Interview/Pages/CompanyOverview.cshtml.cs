using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace B4Interview.Pages
{
    public class CompanyOverviewModel : BaseModel
    {
        public CompanyOverviewModel(DatabaseContext context) : base(context) { }

        [BindProperty(SupportsGet = true)]
        public string Company { get; set; }

        public Company CompanyResult { get; set; }
        public void OnGet()
        {
            CompanyResult = databaseContext.Companies.Where(c => c.Identifier == Company)
                .First();
        }

        public JsonResult OnGetNamesAndPosition(string query, string type)
        {
            if (type.ToUpper() == "JOBS")
            {
                return new JsonResult(
                    databaseContext.Jobs
                    .Where(c => c.Title.ToUpper().Contains(query.ToUpper()) || c.Skills.Any(s => s.Name.ToUpper().Contains(query.ToUpper())))
                    .Select(c => new TypeaheadModel { label = c.Title, category = "JobTitle", identifier = c.Identifier })
                );
            }
            else if (type.ToUpper() == "INTERVIEWS" || type.ToUpper() == "QUESTIONS")
            {
                var companies = GetCompanies(query);
                var skills = GetSkills(query);

                var positions = databaseContext.Interviews
                .Where(c => c.Title.ToUpper().Contains(query.ToUpper()))
                .Select(c => new TypeaheadModel { label = c.Title, category = "InterviewJobTitle", identifier = c.Identifier })
                .Distinct();

                return new JsonResult(companies.Concat(positions).Concat(skills));
            }
            else
            {
                return new JsonResult(GetCompanies(query));
            }
        }

        public JsonResult OnGetNames(string query)
        {
            return new JsonResult(GetCompanies(query).Select(c => c.label));
        }

        public JsonResult OnGetSkills(string query)
        {
            return new JsonResult(GetSkills(query).Select(s => s.label));
        }

        private IQueryable<TypeaheadModel> GetCompanies(string query)
        {
            return databaseContext.Companies
                .Where(c => c.Name.ToUpper().Contains(query.ToUpper())
                            ||
                            c.Identifier.ToUpper().Contains(query.ToUpper()))
                .Select(c => new TypeaheadModel { label = c.Name, category = "Company", identifier = c.Identifier });
        }

        private IQueryable<TypeaheadModel> GetSkills(string query)
        {
            return databaseContext.Skills
            .Where(c => c.Name.ToUpper().Contains(query.ToUpper()))
            .Select(c => new TypeaheadModel { label = c.Name, category = "Skill", identifier = c.Identifier });
        }

        private class TypeaheadModel
        {
            public string label { get; set; }
            public string category { get; set; }
            public string identifier { get; set; }
        }
    }
}