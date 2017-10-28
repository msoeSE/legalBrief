using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract]
    public class Appellant
    {
        [Required]
        public string Name  { get; set; }
        [Required]
        public Address Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}