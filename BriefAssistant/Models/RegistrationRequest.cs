using System.ComponentModel.DataAnnotations;

namespace BriefAssistant.Models
{
    public class RegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
