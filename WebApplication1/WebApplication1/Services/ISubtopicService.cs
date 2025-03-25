using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ISubtopicService
    {
        Task<IEnumerable<Subtopic>> GetAllSubtopicsAsync();
        Task<Subtopic?> GetSubtopicByIdAsync(int id);
        Task<IEnumerable<Subtopic>> GetSubtopicsBySubjectIdAsync(int subjectId);
        Task AddSubtopicAsync(Subtopic subtopic);
        Task UpdateSubtopicAsync(Subtopic subtopic);
        Task DeleteSubtopicAsync(int id);

    }
}
