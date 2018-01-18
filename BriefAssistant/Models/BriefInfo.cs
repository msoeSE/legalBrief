using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class BriefInfo
    {
        public Guid Id { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public AppellateCase AppellateCase { get; set; }
        [Required]
        [DataMember]
        public ContactInfo ContactInfo { get; set; }
        [Required]
        [DataMember]
        public CircuitCourtCase CircuitCourtCase { get; set; }
        [DataMember]
        public string AppellateCourtCaseNumber { get; set; }
        [DataMember]
        public string IssuesPresented { get; set; }
        [DataMember]
        public string OralArgumentStatement { get; set; }
        [Required]
        [DataMember]
        public string PublicationStatement { get; set; }
        [DataMember]
        public string CaseFactsStatement { get; set; }
        [DataMember]
        public string Argument { get; set; }
        [DataMember]
        public string Conclusion { get; set; }
        [DataMember]
        public string AppendixDocuments { get; set; }
    }
}