using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public enum BriefType
    {
        [EnumMember(Value = "Initial")]
        Initial,
        [EnumMember(Value = "Reply")]
        Reply,
        [EnumMember(Value = "Response")]
        Response,
        [EnumMember(Value = "Petition")]
        Petition
    }
}
