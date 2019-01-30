using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class InterviewRound
    {
        public int Id { get; set; }
        public virtual RoundType RoundType { get; set; }
        public string Detail { get; set; }

        public virtual Interview Interview { get; set; }
        public int InterviewId { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }

    public enum RoundType
    {
        Hr,
        Technical
    }
}