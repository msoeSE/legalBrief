using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class BriefInfo
    {
        public int InitialBriefId { get; set; }
        public int CaseId { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        [NotMapped]
        public AppellateCase AppellateCase { get; set; }
        [Required]
        [DataMember]
        public Appellant Appellant { get; set; }
        [Required]
        [DataMember]
        public CircuitCourtCase CircuitCourtCase { get; set; }
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
        public string AppendexDocuments { get; set; }
    }
}
