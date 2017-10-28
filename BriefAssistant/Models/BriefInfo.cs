using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class BriefInfo
    {
        [Required]
        public Appellant Appellant { get; set; }
        [Required]
        public CircutCourtCase CircutCourtCase { get; set; }
        [Required]
        public string IssuesPresented { get; set; }
        [Required]
        public string OralArgumentStatement { get; set; }
        [Required]
        public string PublicationStatement { get; set; }
        [Required]
        public string CaseFactsStatement { get; set; }
        [Required]
        public string Argument { get; set; }
        [Required]
        public string AppendexDocuments { get; set; }
    }
}
