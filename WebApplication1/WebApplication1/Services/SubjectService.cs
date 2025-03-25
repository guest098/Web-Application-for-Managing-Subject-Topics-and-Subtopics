using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllSubjectsAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepository.GetSubjectByIdAsync(id);
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            // Ensure subtopics are correctly linked to the subject
            if (subject.Subtopics != null && subject.Subtopics.Any())
            {
                foreach (var subtopic in subject.Subtopics)
                {
                    subtopic.Subject = subject; // Explicitly set the Subject property
                }
            }

            await _subjectRepository.AddSubjectAsync(subject);
        }


        public async Task UpdateSubjectAsync(Subject subject)
        {
            await _subjectRepository.UpdateSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            await _subjectRepository.DeleteSubjectAsync(id);
        }
    }
}
