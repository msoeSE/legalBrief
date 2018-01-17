using System.ComponentModel.DataAnnotations;

namespace BriefAssistant.Models
{
    public class BriefInfo
    {
        [DataMember]
        public AppellateCase AppellateCase { get; set; }

        [Required]
        public Appellant Appellant { get; set; }

        [Required]
        public CircuitCourtCase CircuitCourtCase { get; set; }
        [Required]
        [DataMember]
        public string AppellateCourtCaseNumber { get; set; }
        [DataMember]
        public string IssuesPresented { get; set; }

        public string OralArgumentStatement { get; set; }

        [Required]
        public string PublicationStatement { get; set; }

        public string CaseFactsStatement { get; set; }

        public string Argument { get; set; }

        public string Conclusion { get; set; }

        public string AppendixDocuments { get; set; }
    }
}