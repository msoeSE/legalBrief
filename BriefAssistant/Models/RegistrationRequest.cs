using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class RegistrationRequest
    {
        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }
        [Required]
        [DataMember]
        public string Password { get; set; }
    }
}
