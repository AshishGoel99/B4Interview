﻿namespace B4Interview.DataLayer.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }
}
