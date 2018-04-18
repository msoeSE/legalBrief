using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class ContactInfo
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string BarId { get; set; }
    }
}