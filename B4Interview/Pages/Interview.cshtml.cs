using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace B4Interview.Pages
{
    public class InterviewModel : BaseModel
    {
        public InterviewModel(DatabaseContext context) : base(context) { }

        public IList<Interview> Interviews { get; set; }

        public void OnGet(string Company, string Position, string Skill)
        {
            var interviews = databaseContext.Interviews
                .Include(i => i.Company)
                .Include(i => i.Rounds)
                .Include("Rounds.Questions")
                .Include(i => i.Votes)
                .Where(i => i.Company != null);

            if (!string.IsNullOrWhiteSpace(Company))
            {
                interviews = interviews
                    .Where(
                    i => i.Company.Identifier == Company);
            }
            else if (!string.IsNullOrWhiteSpace(Position))
            {
                interviews = interviews.Where(i =>
                    i.Identifier == Position);
            }
            else if (!string.IsNullOrWhiteSpace(Skill))
            {
                interviews = interviews
                    .Where(i => i.Rounds
                        .Any(
                            r => r.Questions
                            .Any(
                                q => q.Skill.Identifier.StartsWith(Skill)
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