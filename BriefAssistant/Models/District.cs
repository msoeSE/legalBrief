using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public enum District
    {
        [EnumMember(Value = "I")]
        One,
        [EnumMember(Value = "II")]
        Two,
        [EnumMember(Value = "III")]
        Three,
        [EnumMember(Value = "IV")]
        Four,
        [EnumMember(Value = "V")]
        Five
    }
}
