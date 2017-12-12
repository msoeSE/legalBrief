using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;


namespace BriefAssistant.Models
{
    public class DbCaseInfo
    {
        public int CaseId { get; set; }
        public int UserId { get; set; }
        public County County { get; set; }
        public string CaseNumber { get; set; }
        public Role Role { get; set; }
        public string JudgeFirstName { get; set; }
        public string JudgeLastName { get; set; }
        public string OpponentName { get; set; }
        public DbUserInfo UserInfo { get; set; }
        public DbbriefInfo BriefInfo { get; set; }
    }
}
