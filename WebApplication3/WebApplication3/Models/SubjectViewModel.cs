namespace WebApplication3.Models
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<SubtopicViewModel> Subtopics { get; set; } = new();
    }
}
