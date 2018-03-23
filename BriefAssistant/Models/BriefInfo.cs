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
        public string Argument { get; set; }
        public string Conclusion { get; set; }
        public BriefType Type { get; set; }
    }
}