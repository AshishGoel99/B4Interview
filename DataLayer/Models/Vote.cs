namespace B4Interview.DataLayer.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool UpVote { get; set; }

        public Review Review { get; set; }
        public int ReviewId { get; set; }

        public ApplicationUser Voter { get; set; }
        public string VoterId { get; set; }

        public Interview Interview { get; set; }
        public int InterviewId { get; set; }
    }
}
