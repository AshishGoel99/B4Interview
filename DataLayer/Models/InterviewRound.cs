using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class InterviewRound
    {
        public int Id { get; set; }
        public RoundType RoundType { get; set; }
        public string Detail { get; set; }

        public Interview Interview { get; set; }
        public int InterviewId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }

    public enum RoundType
    {
        Hr,
        Technical
    }
}