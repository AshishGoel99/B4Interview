namespace B4Interview.DataLayer.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public virtual ApplicationUser Applicant { get; set; }
        public string ApplicantId { get; set; }
        public virtual Job Job { get; set; }
        public int JobId { get; set; }
    }
}
