﻿namespace WebApplication3.Models
{
    public class SubtopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubjectId { get; set; }
    }
}
