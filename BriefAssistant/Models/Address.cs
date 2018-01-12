using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public enum State
    {
        [EnumMember]
        AK,
        [EnumMember]
        AL,
        [EnumMember]
        AR,
        [EnumMember]
        AZ,
        [EnumMember]
        CA,
        [EnumMember]
        CO,
        [EnumMember]
        CT,
        [EnumMember]
        DC,
        [EnumMember]
        DE,
        [EnumMember]
        FL,
        [EnumMember]
        GA,
        [EnumMember]
        HI,
        [EnumMember]
        IA,
        [EnumMember]
        ID,
        [EnumMember]
        IL,
        [EnumMember]
        IN,
        [EnumMember]
        KS,
        [EnumMember]
        KY,
        [EnumMember]
        LA,
        [EnumMember]
        MA,
        [EnumMember]
        MD,
        [EnumMember]
        ME,
        [EnumMember]
        MI,
        [EnumMember]
        MN,
        [EnumMember]
        MO,
        [EnumMember]
        MS,
        [EnumMember]
        MT,
        [EnumMember]
        NC,
        [EnumMember]
        ND,
        [EnumMember]
        NE,
        [EnumMember]
        NH,
        [EnumMember]
        NJ,
        [EnumMember]
        NM,
        [EnumMember]
        NV,
        [EnumMember]
        NY,
        [EnumMember]
        OH,
        [EnumMember]
        OK,
        [EnumMember]
        OR,
        [EnumMember]
        PA,
        [EnumMember]
        RI,
        [EnumMember]
        SC,
        [EnumMember]
        SD,
        [EnumMember]
        TN,
        [EnumMember]
        TX,
        [EnumMember]
        UT,
        [EnumMember]
        VA,
        [EnumMember]
        VT,
        [EnumMember]
        WA,
        [EnumMember]
        WI,
        [EnumMember]
        WV,
        [EnumMember]
        WY
    }

    [DataContract(Namespace = "")]
    public class Address
    {

        [Required]
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string Street2 { get; set; }
        [Required]
        [DataMember]
        public string City { get; set; }
        [Required]
        [DataMember]
        public State State { get; set; }

        [Required]
        [RegularExpression(@"\A\d{5}(?:[ \-]\d{4})?")]
        [DataMember]
        public string Zip { get; set; }

    }
}