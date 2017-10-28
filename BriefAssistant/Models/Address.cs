using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public enum State
    {
        AK,
        AL,
        AR,
        AZ,
        CA,
        CO,
        CT,
        DC,
        DE,
        FL,
        GA,
        HI,
        IA,
        ID,
        IL,
        IN,
        KS,
        KY,
        LA,
        MA,
        MD,
        ME,
        MI,
        MN,
        MO,
        MS,
        MT,
        NC,
        ND,
        NE,
        NH,
        NJ,
        NM,
        NV,
        NY,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VA,
        VT,
        WA,
        WI,
        WV,
        WY
    }

    [DataContract]
    public class Address
    {
        [Required]
        public string Street { get; set; }
        [DataMember]
        public string Streee2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public State State { get; set; }

        [Required]
        [RegularExpression(@"\A\d{5}(?:[ \-]\d{4})?")]
        public string Zip { get; set; }
    }
}