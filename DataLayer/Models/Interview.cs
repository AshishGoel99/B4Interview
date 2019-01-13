﻿using System;
using System.Collections.Generic;

namespace B4Interview.DataLayer.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short Difficulty { get; set; }
        public short Source { get; set; }
        public short Rounds { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public DateTime PostedOn { get; set; }
        public decimal UpVote { get; set; }
        public decimal DownVote { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
