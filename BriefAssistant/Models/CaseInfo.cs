using System;
using System.Collections.Generic;

namespace BriefAssistant.Models
{
    public partial class CaseInfo
    {
        public int CaseId { get; set; }
        public int UserId { get; set; }
        public virtual CircuitCourtCase CircuitCourtCase { get;set;}


        public UserInfo User { get; set; }
        public BriefInfo BriefInfo { get; set; }
    }
}
