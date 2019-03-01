using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Detail { get; set; }

        public virtual InterviewRound InterviewRound { get; set; }
        public int InterviewRoundId { get; set; }

        public virtual Skill Skill { get; set; }
        public int? SkillId { get; set; }
        public decimal UpVote { get; set; }
        public decimal DownVote { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}