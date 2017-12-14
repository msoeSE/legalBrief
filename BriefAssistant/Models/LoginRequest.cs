using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DataMember]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}