﻿
namespace WebApplication1.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Subtopic> Subtopics { get; set; } = new List<Subtopic>();
    }
}
