using System;
using System.Collections.Generic;


namespace BriefAssistant.Models
{
    public partial class InitialBriefInfo
    {
        public int InitialBriefId { get; set; }
        public int CaseId { get; set; }
        public virtual BriefInfo Briefinfo { get; set; }

        public CaseInfo Case { get; set; }
    }
}
