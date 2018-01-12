using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Brief> BriefRecord { get; set; }

    }
}