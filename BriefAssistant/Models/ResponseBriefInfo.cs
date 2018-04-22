using System;

namespace BriefAssistant.Models
{
    public class ResponseBriefInfo
    {
        public Guid Id { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }

        public BriefInfo BriefInfo { get; set; }
    }
}