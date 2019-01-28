﻿using B4Interview.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class InterviewModel : BaseModel
    {
        public InterviewModel(DatabaseContext context) : base(context) { }

        public IList<Interview> Interviews { get; set; }

        public void OnGet(string company, string skill)
        {
            var interviews = databaseContext.Interviews
                .OrderByDescending(i => i.PostedOn);

            if (!string.IsNullOrWhiteSpace(company))
            {
                Interviews = interviews
                    .Where(i => i.Company.Name == company || i.Company.Identifier == company)
                    .ToList();
            }
            else if (!string.IsNullOrWhiteSpace(skill))
            {
                Interviews = interviews
                    .Where(i => i.Rounds
                        .Any(
                            r => r.Questions
                            .Any(
                                q => q.Skill.Name.ToUpper() == skill.ToUpper()
                                )
                            )
                        )
                    .ToList();
            }
            else
            {
                Interviews = interviews.ToList();
            }
        }
    }
}