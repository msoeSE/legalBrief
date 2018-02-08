using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class ContactInfo
    {
        [Required]
        [DataMember]
        public string Name { get; set; }
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