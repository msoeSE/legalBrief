using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public enum County
    {
        Adams,
        Ashland,
        Barron,
        Bayfield,
        Brown,
        Buffalo,
        Burnett,
        Calumet,
        Chippewa,
        Clark,
        Columbia,
        Crawford,
        Dane,
        Dodge,
        Door,
        Douglas,
        Dunn,
        [EnumMember(Value = "Eau Claire")]
        EauClaire,
        Florence,
        [EnumMember(Value = "Fond Du Lac")]
        FondDuLac,
        Forest,
        Grant,
        Green,
        [EnumMember(Value = "Green Lake")]
        GreenLake,
        Iowa,
        Iron,
        Jackson,
        Jefferson,
        Juneau,
        Kenosha,
        Kewaunee,
        LaCrosse,
        Lafayette,
        Langlade,
        Lincoln,
        Manitowoc,
        Marathon,
        Marinette,
        Marquette,
        Menominee,
        Milwaukee,
        Monroe,
        Oconto,
        Oneida,
        Outagamie,
        Ozaukee,
        Pepin,
        Pierce,
        Polk,
        Portage,
        Price,
        Racine,
        Richland,
        Rock,
        Rusk,
        Sauk,
        Sawyer,
        Shawano,
        Sheboygan,
        [EnumMember(Value="St. Croix")]
        StCroix,
        Taylor,
        Trempealeau,
        Vernon,
        Vilas,
        Walworth,
        Washburn,
        Washington,
        Waukesha,
        Waupaca,
        Waushara,
        Winnebago,
        Wood
    }

    [DataContract]
    public enum Role
    {
        Plantiff,
        Defendent
    }

    [DataContract]
    public class CircutCourtCase
    {
        [Required]
        public County County { get; set; }
        [Required]
        [RegularExpression(@"\A\d{4}[ \-]?[A-Za-z]{2}[ \-]?\d{6}")]
        public string CaseNumber { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string JudgeFirstName { get; set; }
        [Required]
        public string JudgeLastName { get; set; }
        [Required]
        public string OpponentName { get; set; }
    }
}
