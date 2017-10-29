using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract]
    public class BriefInfo
    {
        
        [Required]
        [DataMember]
        public Appellant Appellant { get; set; }
        [Required]
        [DataMember]
        public CircutCourtCase CircutCourtCase { get; set; }
        [Required]
        [DataMember]
        public string IssuesPresented { get; set; }
        [Required]
        [DataMember]
        public string OralArgumentStatement { get; set; }
        [Required]
        [DataMember]
        public string PublicationStatement { get; set; }
        [Required]
        [DataMember]
        public string CaseFactsStatement { get; set; }
        [Required]
        [DataMember]
        public string Argument { get; set; }
        [Required]
        [DataMember]
        public string AppendexDocuments { get; set; }
    }
}
