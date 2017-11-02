using System.ComponentModel.DataAnnotations;

namespace BriefAssistant.Models
{
    public class EmailRequest
    {
        [EmailAddress]
        [Required]
        private string Email { get; set; }
    }
}
