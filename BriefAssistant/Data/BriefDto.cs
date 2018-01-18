using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BriefAssistant.Data
{
    public class BriefDto : IUserData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid CaseId { get; set; }
        public Guid ContactInfoId { get; set; }

        public string Name { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }
        public string AppellateCourtCaseNumber { get; set; }
        public string Argument { get; set; }
        public string Conclusion { get; set; }
        public string AppendixDocuments { get; set; }

        public ContactInfoDto ContactInfoDto { get; set; }
        public CaseDto CaseDto { get; set; }
    }
}
