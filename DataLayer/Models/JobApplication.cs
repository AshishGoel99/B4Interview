namespace B4Interview.DataLayer.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public ApplicationUser Applicant { get; set; }
        public string ApplicantId { get; set; }
        public Job Job { get; set; }
        public int JobId { get; set; }
    }
}
