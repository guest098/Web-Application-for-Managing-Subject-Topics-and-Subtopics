using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class SubtopicService : ISubtopicService
    {
        private readonly ISubtopicRepository _subtopicRepository;

        public SubtopicService(ISubtopicRepository subtopicRepository)
        {
            _subtopicRepository = subtopicRepository;
        }

        public async Task<IEnumerable<Subtopic>> GetAllSubtopicsAsync()
        {
            return await _subtopicRepository.GetAllSubtopicsAsync();
        }

        public async Task<Subtopic?> GetSubtopicByIdAsync(int id)
        {
            return await _subtopicRepository.GetSubtopicByIdAsync(id);
        }

        public async Task<IEnumerable<Subtopic>> GetSubtopicsBySubjectIdAsync(int subjectId)
        {
            return await _subtopicRepository.GetSubtopicsBySubjectIdAsync(subjectId);
        }

        public async Task AddSubtopicAsync(Subtopic subtopic)
        {
            await _subtopicRepository.AddSubtopicAsync(subtopic);
        }

        public async Task UpdateSubtopicAsync(Subtopic subtopic)
        {
            await _subtopicRepository.UpdateSubtopicAsync(subtopic);
        }

        public async Task DeleteSubtopicAsync(int id)
        {
            await _subtopicRepository.DeleteSubtopicAsync(id);
        }
    }
}
