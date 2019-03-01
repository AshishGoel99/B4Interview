using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }

        public virtual Job Job { get; set; }
        public int? JobId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        //public int? QuestionId { get; set; }
    }
}