namespace B4Interview.DataLayer.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
