using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class DbbriefInfo
    {
        public int InitialBriefId { get; set; }
        public int CaseId { get; set; }
        public int UserId { get; set; }
        public DbCaseInfo CaseInfo { get; set; }
        public DbUserInfo UserInfo { get; set; }
        public string IssuesPresented { get; set; }
        public string OralArgumentStatement { get; set; }
        public string PublicationStatement { get; set; }
        public string CaseFactsStatement { get; set; }
        public string Argument { get; set; }
        public string Conclusion { get; set; }
        public string AppendexDocuments { get; set; }
    }
}
