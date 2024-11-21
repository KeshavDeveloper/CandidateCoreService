using CandidateCoreService.Model;
using CandidateCoreService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CandidateCoreService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _repository;

        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCandidate = await _repository.GetByEmailAsync(candidateDto.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidateDto.FirstName;
                existingCandidate.LastName = candidateDto.LastName;
                existingCandidate.PhoneNumber = candidateDto.PhoneNumber;
                existingCandidate.BestCallTime = candidateDto.BestCallTime;
                existingCandidate.LinkedInUrl = candidateDto.LinkedInUrl;
                existingCandidate.GitHubUrl = candidateDto.GitHubUrl;
                existingCandidate.Comment = candidateDto.Comment;
                existingCandidate.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(existingCandidate);
                return Ok(existingCandidate);
            }
            else
            {
                var newCandidate = new Candidate
                {
                    FirstName = candidateDto.FirstName,
                    LastName = candidateDto.LastName,
                    Email = candidateDto.Email,
                    PhoneNumber = candidateDto.PhoneNumber,
                    BestCallTime = candidateDto.BestCallTime,
                    LinkedInUrl = candidateDto.LinkedInUrl,
                    GitHubUrl = candidateDto.GitHubUrl,
                    Comment = candidateDto.Comment,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _repository.AddAsync(newCandidate);
                return CreatedAtAction(nameof(AddOrUpdateCandidate), newCandidate);
            }
        }
    }

}
