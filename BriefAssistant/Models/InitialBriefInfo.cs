using System;

namespace BriefAssistant.Models
{
    public class InitialBriefInfo
    {
        public Guid Id { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }
        public string AppendixDocuments { get; set; }

        public BriefInfo BriefInfo { get; set; }
    }
}