using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace BriefAssistant.Services
{
    public class AuthMessageSenderOptions
    {
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
