using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
namespace WebApplication1.Repositories
{
    public class SubjectRepository: ISubjectRepository
    {
        private readonly AppDbContext _context;
        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.Include(s => s.Subtopics).ToListAsync();
        }
        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Subtopics)  // ✅ Include Subtopics
                .ThenInclude(st => st.Subject) // ✅ Ensure Subtopics have their Subject loaded
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
    }
}
