using System.ComponentModel.DataAnnotations;

namespace BriefAssistant.Models
{
    public class EmailRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
