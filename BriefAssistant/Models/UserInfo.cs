using System;
using System.Collections.Generic;

namespace BriefAssistant.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public int Password { get; set; }
        public virtual Appellant Appellant { get; set; }
        public CircuitCourtCase CircuitCourtCase { get; set; }
    }
}
