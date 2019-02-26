using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class InterviewModel : BaseModel
    {
        public InterviewModel(DatabaseContext context) : base(context) { }

        public IList<Interview> Interviews { get; set; }

        public void OnGet(string search, string skill)
        {
            var interviews = databaseContext.Interviews
                .Where(i => i.Company != null);

            if (!string.IsNullOrWhiteSpace(search))
            {
                interviews = interviews
                    .Where(
                    i => i.Company.Name == search || i.Company.Identifier == search
                    ||
                    i.Title == search
                    ||
                    i.Rounds
                        .Any(
                            r => r.Questions
                            .Any(
                                q => q.Skill.Name.StartsWith(search)
                                )
                            )
                    );
            }
            else if (!string.IsNullOrWhiteSpace(skill))
            {
                interviews = interviews
                    .Where(i => i.Rounds
                        .Any(
                            r => r.Questions
                            .Any(
                                q => q.Skill.Name.ToUpper() == skill.ToUpper()
                                )
                            )
                        );
            }

            Interviews = GetPagedData(interviews
                .OrderByDescending(i => i.PostedOn)).ToList();
        }

        #region UpVote
        public IActionResult OnGetUpVote(int id)
        {
            var userId = UserId;
            var interview = databaseContext.Interviews
                .Single(r => r.Id == id);

            interview.UpVote++;
            interview.Votes.Add(new Vote
            {
                UpVote = true,
                InterviewId = id,
                VoterId = userId
            });

            databaseContext.SaveChanges();

            return new OkResult();
        }
        #endregion

        #region DownVote
        public IActionResult OnGetDownVote(int id)
        {
            var userId = UserId;
            var interview = databaseContext.Interviews
                .Single(r => r.Id == id);

            interview.DownVote++;
            interview.Votes.Add(new Vote
            {
                UpVote = false,
                InterviewId = id,
                VoterId = userId
            });

            databaseContext.SaveChanges();

            return new OkResult();
        }
        #endregion
    }
}