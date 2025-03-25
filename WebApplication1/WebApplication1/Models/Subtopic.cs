namespace WebApplication1.Models
{
    public class Subtopic
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }  // 👈 Allow null

    }
}
