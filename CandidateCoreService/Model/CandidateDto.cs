using System.ComponentModel.DataAnnotations;

namespace CandidateCoreService.Model
{
    public class CandidateDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BestCallTime { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Required] public string Comment { get; set; }
    }

}
