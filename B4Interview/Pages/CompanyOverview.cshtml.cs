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
            switch (type.ToUpper())
            {
                case "JOBS":
                    {
                        var companies = GetCompanies(query);
                        var skills = GetSkills(query);
                        var positions = GetJobPositions(query);

                        return new JsonResult(companies.Concat(positions).Concat(skills).Concat(positions));
                    }
                case "QUESTIONS":
                case "INTERVIEWS":
                    {
                        var companies = GetCompanies(query);
                        var skills = GetSkills(query);
                        var positions = GetInterviewPositions(query);

                        return new JsonResult(companies.Concat(positions).Concat(skills));
                    }
                default:
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

        private IQueryable<TypeaheadModel> GetInterviewPositions(string query)
        {
            return databaseContext.Interviews
                .Where(c => c.Title.ToUpper().Contains(query.ToUpper()))
                .Select(c => new TypeaheadModel { label = c.Title, category = "InterviewJobTitle", identifier = c.Identifier })
                .Distinct();
        }

        private IQueryable<TypeaheadModel> GetJobPositions(string query)
        {
            return databaseContext.Jobs
                .Where(c => c.Title.ToUpper().Contains(query.ToUpper()))
                .Select(c => new TypeaheadModel { label = c.Title, category = "JobTitle", identifier = c.Identifier })
                .Distinct();
        }

        private class TypeaheadModel
        {
            public string label { get; set; }
            public string category { get; set; }
            public string identifier { get; set; }
        }
    }
}