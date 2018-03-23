using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public IList<ContactInfoDto> Contacts { get; set; }
        public IList<BriefDto> Briefs { get; set; }
        public IList<InitialBriefDto> Initials { get; set; }
        public IList<ReplyBriefDto> Replies { get; set; }
        public IList<CircuitCourtCaseDto> Cases { get; set; }
    }
}