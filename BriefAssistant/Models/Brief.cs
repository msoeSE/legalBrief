using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class Brief
    {
        public String Name { get; set; }
        public int BriefId { get; set; }
        public String Id { get; set; }
        public ApplicationUser User { get; set; }
        public int CaseId { get; set; }
        public int UserInfoId { get; set; }
        public int InitialBriefInfoId { get; set; }
        public DbbriefInfo BriefInfo { get; set; }
        public DbUserInfo UserInfo { get; set; }
        public DbCaseInfo CaseInfo { get; set; }
    }
}
