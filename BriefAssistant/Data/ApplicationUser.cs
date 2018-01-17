using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public IList<ContactInfoDto> Contacts { get; set; }
        public IList<BriefDto> Briefs { get; set; }
        public IList<CaseDto> Cases { get; set; }
    }
}