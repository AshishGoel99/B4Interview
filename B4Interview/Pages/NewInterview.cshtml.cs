using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace B4Interview.Pages
{
    public class NewInterviewModel : BaseModel
    {

        public NewInterviewModel(DatabaseContext context) : base(context)
        { }

        public void OnGet()
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Company { get; set; }
            [Required]
            public string Experience { get; set; }
            [Required]
            public string Location { get; set; }
            public string Description { get; set; }
            public short Source { get; set; }
            public IList<InputRound> Rounds { get; set; }
        }

        public class InputRound
        {
            public short Type { get; set; }
            public string Desc { get; set; }
            public IList<InputQuestion> Questions { get; set; }
        }

        public class InputQuestion
        {
            public string Question { get; set; }
            public string Skill { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = UserId;

            var company = databaseContext.Companies
                .Where(c => c.Name.ToUpper().Contains(Input.Company) || c.Identifier.ToUpper().Contains(Input.Company));

            int companyId = 0;
            if (company.Any())
                companyId = company.Single().Id;
            else
                companyId = CreateCompany(Input.Company).Id;

            var interview = new Interview
            {
                CandidateId = userId,
                CompanyId = companyId,
                Experience = Input.Experience,
                Location = Input.Location,
                PostedOn = DateTime.Now,
                Source = Input.Source,
                Title = Input.Title,
                Rounds = new List<InterviewRound>()
            };

            if (Input.Rounds != null && Input.Rounds.Any())
            {
                foreach (var round in Input.Rounds)
                {
                    var iRound = new InterviewRound
                    {
                        Detail = round.Desc,
                        RoundType = round.Type
                    };

                    if (round.Questions != null && round.Questions.Any())
                    {
                        foreach (var ques in round.Questions)
                        {
                            var iQues = new Question
                            {
                                Detail = ques.Question
                            };

                            if (!string.IsNullOrWhiteSpace(ques.Skill))
                            {
                                var skillInDb = databaseContext.Skills.Where(s => s.Name == ques.Skill);

                                Skill skill;
                                if (!skillInDb.Any())
                                    skill = new Skill
                                    {
                                        Name = ques.Skill
                                    };
                                else
                                    skill = skillInDb.Single();

                                iQues.Skill = skill;
                            }

                            iRound.Questions.Add(iQues);
                        }
                    }

                    interview.Rounds.Add(iRound);
                }
            }

            databaseContext.Interviews.Add(interview);
            databaseContext.SaveChanges();

            return new RedirectToPageResult("Interviews");
        }
    }

    public enum InterviewSource
    {
        Referral,
        LinkedIn
    }

    public enum RoundType
    {
        Hr,
        Technical
    }
}