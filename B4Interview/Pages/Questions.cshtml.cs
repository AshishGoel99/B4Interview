using System.Collections.Generic;
using System.Linq;
using B4Interview.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace B4Interview.Pages
{
    public class QuestionsModel : BaseModel
    {
        public QuestionsModel(DatabaseContext context) : base(context)
        {
        }

        public ICollection<Question> Questions { get; set; }
        public void OnGet(int Id, string Skill, string Company, string Position)
        {
            IQueryable<Question> questionsQuery = null;

            if (Id > 0)
                Questions = databaseContext.Questions.Where(q => q.Id == Id).ToList();

            else if (!string.IsNullOrWhiteSpace(Skill))
                questionsQuery = databaseContext.Questions
                    .Where(q => q.Skill.Identifier == Skill);

            else if (!string.IsNullOrWhiteSpace(Company))
                questionsQuery = databaseContext.Questions
                    .Where(q => q.InterviewRound.Interview.Company.Identifier == Company);

            else if (!string.IsNullOrWhiteSpace(Position))
                questionsQuery = databaseContext.Questions
                    .Where(q => q.InterviewRound.Interview.Identifier == Position);
            else
                questionsQuery = databaseContext.Questions;

            Questions = GetPagedData(questionsQuery.OrderByDescending(q => q.UpVote)).ToList();
        }

        #region UpVote
        public IActionResult OnGetUpVote(int id)
        {
            var userId = UserId;
            var question = databaseContext.Questions
                .First(r => r.Id == id);

            question.UpVote++;
            question.Votes.Add(new Vote
            {
                UpVote = true,
                QuestionId = id,
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
            var question = databaseContext.Questions
                .First(r => r.Id == id);

            question.DownVote++;
            question.Votes.Add(new Vote
            {
                UpVote = false,
                QuestionId = id,
                VoterId = userId
            });

            databaseContext.SaveChanges();

            return new OkResult();
        }
        #endregion
    }
}