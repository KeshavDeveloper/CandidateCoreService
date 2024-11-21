using CandidateCoreService.Model;
using Microsoft.EntityFrameworkCore;

namespace CandidateCoreService.Service
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByEmailAsync(string email) =>
            await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
