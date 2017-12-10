using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class Appellant
    {
        public int UserId { get; set; }
        public int Password { get; set; }
        public CircuitCourtCase CircuitCourtCase { get; set; }
        public BriefInfo BriefInfo { get; set; }
        [Required]
        [DataMember]
        public string Name  { get; set; }
        [Required]
        [DataMember]
        public Address Address { get; set; }

        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [DataMember]
        public string Phone { get; set; }
    }
}