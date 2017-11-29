using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public enum County
    {
        [EnumMember]
        Adams,
        [EnumMember]
        Ashland,
        [EnumMember]
        Barron,
        [EnumMember]
        Bayfield,
        [EnumMember]
        Brown,
        [EnumMember]
        Buffalo,
        [EnumMember]
        Burnett,
        [EnumMember]
        Calumet,
        [EnumMember]
        Chippewa,
        [EnumMember]
        Clark,
        [EnumMember]
        Columbia,
        [EnumMember]
        Crawford,
        [EnumMember]
        Dane,
        [EnumMember]
        Dodge,
        [EnumMember]
        Door,
        [EnumMember]
        Douglas,
        [EnumMember]
        Dunn,
        [EnumMember(Value = "Eau Claire")]
        EauClaire,
        [EnumMember]
        Florence,
        [EnumMember(Value = "Fond Du Lac")]
        FondDuLac,
        [EnumMember]
        Forest,
        [EnumMember]
        Grant,
        [EnumMember]
        Green,
        [EnumMember(Value = "Green Lake")]
        GreenLake,
        [EnumMember]
        Iowa,
        [EnumMember]
        Iron,
        [EnumMember]
        Jackson,
        [EnumMember]
        Jefferson,
        [EnumMember]
        Juneau,
        [EnumMember]
        Kenosha,
        [EnumMember]
        Kewaunee,
        [EnumMember]
        LaCrosse,
        [EnumMember]
        Lafayette,
        [EnumMember]
        Langlade,
        [EnumMember]
        Lincoln,
        [EnumMember]
        Manitowoc,
        [EnumMember]
        Marathon,
        [EnumMember]
        Marinette,
        [EnumMember]
        Marquette,
        [EnumMember]
        Menominee,
        [EnumMember]
        Milwaukee,
        [EnumMember]
        Monroe,
        [EnumMember]
        Oconto,
        [EnumMember]
        Oneida,
        [EnumMember]
        Outagamie,
        [EnumMember]
        Ozaukee,
        [EnumMember]
        Pepin,
        [EnumMember]
        Pierce,
        [EnumMember]
        Polk,
        [EnumMember]
        Portage,
        [EnumMember]
        Price,
        [EnumMember]
        Racine,
        [EnumMember]
        Richland,
        [EnumMember]
        Rock,
        [EnumMember]
        Rusk,
        [EnumMember]
        Sauk,
        [EnumMember]
        Sawyer,
        [EnumMember]
        Shawano,
        [EnumMember]
        Sheboygan,
        [EnumMember(Value="St. Croix")]
        StCroix,
        [EnumMember]
        Taylor,
        [EnumMember]
        Trempealeau,
        [EnumMember]
        Vernon,
        [EnumMember]
        Vilas,
        [EnumMember]
        Walworth,
        [EnumMember]
        Washburn,
        [EnumMember]
        Washington,
        [EnumMember]
        Waukesha,
        [EnumMember]
        Waupaca,
        [EnumMember]
        Waushara,
        [EnumMember]
        Winnebago,
        [EnumMember]
        Wood
    }

    [DataContract(Namespace = "")]
    public enum Role
    {
        [EnumMember]
        Plaintiff,
        [EnumMember]
        Defendent,
        [EnumMember]
        Respondent,
        [EnumMember]
        Petitioner
    }

    [DataContract(Namespace = "")]
    public class CircuitCourtCase
    {
        [Required]
        [DataMember]
        public County County { get; set; }
        [Required]
        [DataMember]
        public string CaseNumber { get; set; }
        [Required]
        [DataMember]
        public Role Role { get; set; }
        [Required]
        [DataMember]
        public string JudgeFirstName { get; set; }
        [Required]
        [DataMember]
        public string JudgeLastName { get; set; }
        [Required]
        [DataMember]
        public string OpponentName { get; set; }
        [Required]
        [DataMember]
        public Role OpponentRole { get; set; }
    }
}
