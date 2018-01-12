using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("InitialBriefInfoId")]
        public DbbriefInfo BriefInfo { get; set; }
        [ForeignKey("UserInfoId")]
        public DbUserInfo UserInfo { get; set; }
        [ForeignKey("CaseId")]
        public DbCaseInfo CaseInfo { get; set; }
    }
}
