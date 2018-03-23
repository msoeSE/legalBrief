using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class InitialBriefDto : IUserData
    {
        [Key]
        [ForeignKey(nameof(BriefDto))]
        public Guid BriefId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }
        public string AppendixDocuments { get; set; }

        public BriefDto BriefDto { get; set; }
    }
}
