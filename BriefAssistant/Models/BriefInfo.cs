using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    public class BriefInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        [Required]
        public ContactInfo ContactInfo { get; set; }
        [Required]
        public CircuitCourtCase CircuitCourtCase { get; set; }
        public string AppellateCourtCaseNumber { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }
        public string Argument { get; set; }
        public string Conclusion { get; set; }
        public string AppendixDocuments { get; set; }
    }
}