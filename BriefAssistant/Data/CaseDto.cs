using System;
using System.Collections.Generic;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class CaseDto : IUserData
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public County County { get; set; }
        public string CaseNumber { get; set; }
        public Role Role { get; set; }
        public string JudgeFirstName { get; set; }
        public string JudgeLastName { get; set; }
        public string OpponentName { get; set; }

        public IList<BriefDto> BriefDto { get; set; }
    }
}
