namespace B4Interview.DataLayer.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Detail { get; set; }

        public Skill Skill { get; set; }
        public int SkillId { get; set; }
    }
}