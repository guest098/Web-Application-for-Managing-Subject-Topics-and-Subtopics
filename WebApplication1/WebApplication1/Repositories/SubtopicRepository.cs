using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
namespace WebApplication1.Repositories
{
    public class SubtopicRepository:ISubtopicRepository
    {
        private readonly AppDbContext _context;
        public SubtopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subtopic>> GetAllSubtopicsAsync()
        {
            return await _context.Subtopics
                .Include(st => st.Subject)  // ✅ Ensure Subject is loaded in Subtopics
                .ToListAsync();
        }


        public async Task<Subtopic?> GetSubtopicByIdAsync(int id)
        {
            return await _context.Subtopics.Include(s => s.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Subtopic>> GetSubtopicsBySubjectIdAsync(int subjectId)
        {
            return await _context.Subtopics.Where(s => s.SubjectId == subjectId).ToListAsync();
        }

        public async Task AddSubtopicAsync(Subtopic subtopic)
        {
            await _context.Subtopics.AddAsync(subtopic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubtopicAsync(Subtopic subtopic)
        {
            _context.Subtopics.Update(subtopic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubtopicAsync(int id)
        {
            var subtopic = await _context.Subtopics.FindAsync(id);
            if (subtopic != null)
            {
                _context.Subtopics.Remove(subtopic);
                await _context.SaveChangesAsync();
            }
        }

    }
}
