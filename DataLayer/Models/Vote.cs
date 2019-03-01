namespace B4Interview.DataLayer.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool UpVote { get; set; }

        public virtual Review Review { get; set; }
        public int? ReviewId { get; set; }

        public virtual ApplicationUser Voter { get; set; }
        public string VoterId { get; set; }

        public virtual Interview Interview { get; set; }
        public int? InterviewId { get; set; }

        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }
    }
}
