using B4Interview.DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class IndexModel : BaseModel
    {
        public IndexModel(DatabaseContext context) : base(context)
        { }


        public IList<Job> RecentReferralJobs { get; set; }

        public void OnGet()
        {
            var jobs = databaseContext.Jobs.Where(j => !j.InActive);

            var userId = UserId;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                jobs = jobs.Where(j => j.ReferrerId != userId);
            }

            RecentReferralJobs = jobs.OrderByDescending(j => j.PostedOn).Take(5).ToList();

            //addCompanies();
            //addInterviews();
            //addReviews();
        }

        private void addInterviews()
        {
            var data = System.IO.File.ReadAllText(@"C:\Users\Ashish\Desktop\Interviews(Ambition).json");
            var interviews = JsonConvert.DeserializeObject<IEnumerable<interview>>(data);

            var groups = interviews.GroupBy(g => g.Company);

            foreach (var group in groups)
            {
                var companyId = databaseContext.Companies.SingleOrDefault(c => c.Name == group.Key || c.Identifier.ToUpper() == group.Key.ToUpper())?.Id;

                if (!companyId.HasValue)
                {
                    continue;
                }

                foreach (var interview in group)
                {
                    var intvw = new Interview
                    {
                        CompanyId = companyId.Value,
                        Title = interview.JobTitle,
                        Description = interview.Description ?? interview.OverAllExp,
                        PostedOn = string.IsNullOrWhiteSpace(interview.PostedOn) ? DateTime.MinValue : DateTime.ParseExact(interview.PostedOn, "dd MMM yyyy", null),
                        Rounds = new List<InterviewRound>()
                    };

                    foreach (var round in interview.Rounds)
                    {
                        var r = new InterviewRound
                        {
                            Detail = round.Description,
                            RoundName = round.Name,
                            Questions = new List<Question>()
                        };

                        foreach (var q in round.Questions)
                        {
                            var ques = new Question
                            {
                                Detail = q.Desc
                            };
                            r.Questions.Add(ques);
                        }
                        intvw.Rounds.Add(r);
                    }

                    databaseContext.Interviews.Add(intvw);
                }
            }
            databaseContext.SaveChanges();
        }

        private void addReviews()
        {
            var data = System.IO.File.ReadAllText(@"C:\Users\Ashish\Documents\Devart\dbForge Studio for SQL Server\Export\Reviews 20190317 2135.json");
            var reviews = JsonConvert.DeserializeObject<IEnumerable<review>>(data);

            var groups = reviews.GroupBy(r => r.Company);

            foreach (var group in groups)
            {
                var companyId = databaseContext.Companies.Single(c => c.Name == group.Key).Id;

                foreach (var review in group)
                {
                    databaseContext.Reviews.Add(new Review
                    {
                        AutherInfo = review.AboutEmployee,
                        CompanyId = companyId,
                        Pros = review.Pros,
                        Cons = review.Cons,
                        CreatedOn = DateTime.ParseExact(review.PostedOn, "dd-MM-yyyy", null),
                        Rating = review.Rating,
                        Description = review.Description,
                        Anonymous = true
                    });
                }
            }

            databaseContext.SaveChanges();
        }

        private void addCompanies()
        {
            var data = System.IO.File.ReadAllText(@"C:\Users\Ashish\Documents\Devart\dbForge Studio for SQL Server\Export\expdata 20190317 2114.json");
            var companies = JsonConvert.DeserializeObject<IEnumerable<comp>>(data);

            foreach (var company in companies)
            {
                databaseContext.Companies.Add(new Company
                {
                    EmployeeStrength = company.Size,
                    Name = company.Company,
                    Headquarter = company.Headquarters,
                    Sector = company.Sector,
                    WebSite = company.WebSite,
                    Logo = company.Logo,
                    Identifier = GetIdentifier(company.Company)
                });
            }

            databaseContext.SaveChanges();
        }

        private class comp
        {
            public string Company { get; set; }
            public string Size { get; set; }
            public string WebSite { get; set; }
            public string Headquarters { get; set; }
            public string Sector { get; set; }
            public string Logo { get; set; }
        }

        private class review
        {
            public string AboutEmployee { get; set; }
            public string PostedOn { get; set; }
            public string Company { get; set; }
            public string Pros { get; set; }
            public string Cons { get; set; }
            public string Description { get; set; }
            public float Rating { get; set; }
        }

        private class interview
        {
            public string PostedOn { get; set; }
            public string AboutEmployee { get; set; }
            public string Description { get; set; }
            public string OverAllExp { get; set; }
            public string JobTitle { get; set; }
            public List<InterviewRoundModel> Rounds { get; set; }
            public string Company { get; set; }
            public string WebSite { get; set; }
        }

        internal class InterviewRoundModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<QuestionModel> Questions { get; set; }
        }

        internal class QuestionModel
        {
            public string Desc { get; set; }
        }
    }
}
