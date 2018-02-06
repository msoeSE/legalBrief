using System.ComponentModel;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class BriefExport
    {
        [DataMember]
        public AppellateCase AppellateCase { get; set; }
        [DataMember]
        public ContactInfo ContactInfo { get; set; }
        [DataMember]
        public CircuitCourtCase CircuitCourtCase { get; set; }
        [DataMember]
        public string AppellateCourtCaseNumber { get; set; }
        [DataMember]
        public string IssuesPresented { get; set; }
        [DataMember]
        public string OralArgumentStatement { get; set; }
        [DataMember]
        public string PublicationStatement { get; set; }
        [DataMember]
        public string CaseFactsStatement { get; set; }
        [DataMember]
        public string Argument { get; set; }
        [DataMember]
        public string Conclusion { get; set; }
        [DataMember]
        public string AppendixDocuments { get; set; }
        [DataMember]
        public string TopRole { get; set; }
        [DataMember]
        public string TopName { get; set; }
        [DataMember]
        public string BottomRole { get; set; }
        [DataMember]
        public string BottomName { get; set; }

        /// <summary>
        /// This is the constructor for the BriefExport class. This class is used in the generation of the word document
        /// </summary>
        /// <param name="info">The information of the brief retrieved from the user on the form</param>
        public BriefExport(BriefInfo info)
        {
            ContactInfo = info.ContactInfo;
            CircuitCourtCase = info.CircuitCourtCase;
            AppellateCourtCaseNumber = info.AppellateCourtCaseNumber;
            IssuesPresented = info.IssuesPresented;
            OralArgumentStatement = info.OralArgumentStatement;
            PublicationStatement = info.PublicationStatement;
            CaseFactsStatement = info.CaseFactsStatement;
            Argument = info.Argument;
            Conclusion = info.Conclusion;
            AppendixDocuments = info.AppendixDocuments;

            SetTopAndBottomNamesAndRoles(CircuitCourtCase.Role);
            AppellateCase.District = GetDistrictFromCounty(CircuitCourtCase.County);
        }

        /// <summary>
        /// This method determines what order the appellant and opponent name and roles appear on the brief
        /// </summary>
        /// <param name="role">The role of the appellant</param>
        private void SetTopAndBottomNamesAndRoles(Role role)
        {
            switch (role)
            {
                case Role.Plaintiff:
                    TopName = ContactInfo.Name;
                    BottomName = CircuitCourtCase.OpponentName;
                    TopRole = "Plaintiff-Appellant";
                    BottomRole = "Defendant-Respondent";
                    break;
                case Role.Petitioner:
                    TopName = ContactInfo.Name;
                    BottomName = CircuitCourtCase.OpponentName;
                    TopRole = "Petitioner-Appellant";
                    BottomRole = "Respondent-Respondent";
                    break;
                case Role.Defendent:
                    TopName = CircuitCourtCase.OpponentName;
                    BottomName = ContactInfo.Name;
                    TopRole = "Plaintiff-Respondent";
                    BottomRole = "Defendant-Appellant";
                    break;
                case Role.Respondent:
                    TopName = CircuitCourtCase.OpponentName;
                    BottomName = ContactInfo.Name;
                    TopRole = "Petitioner-Respondent";
                    BottomRole = "Respondent-Appellant";
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(role), (int)role, typeof(Role));
            }
        }

        /// <summary>
        /// This method determines which district to put on the brief based on the county of the circuit court case
        /// </summary>
        /// <param name="county">The county that the circuit court case took place in</param>
        /// <returns></returns>
        private District GetDistrictFromCounty(County county)
        {
            switch (county)
            {
                case County.Milwaukee:
                    return District.One;
                case County.Calumet:
                case County.FondDuLac:
                case County.GreenLake:
                case County.Kenosha:
                case County.Manitowoc:
                case County.Ozaukee:
                case County.Racine:
                case County.Sheboygan:
                case County.Walworth:
                case County.Washington:
                case County.Waukesha:
                case County.Winnebago:
                    return District.Two;
                case County.Ashland:
                case County.Barron:
                case County.Bayfield:
                case County.Brown:
                case County.Buffalo:
                case County.Burnett:
                case County.Chippewa:
                case County.Door:
                case County.Douglas:
                case County.Dunn:
                case County.EauClaire:
                case County.Florence:
                case County.Forest:
                case County.Iron:
                case County.Kewaunee:
                case County.Langlade:
                case County.Lincoln:
                case County.Marathon:
                case County.Marinette:
                case County.Menominee:
                case County.Oconto:
                case County.Oneida:
                case County.Outagamie:
                case County.Pepin:
                case County.Pierce:
                case County.Polk:
                case County.Price:
                case County.Rusk:
                case County.Sawyer:
                case County.Shawano:
                case County.StCroix:
                case County.Taylor:
                case County.Trempealeau:
                case County.Vilas:
                case County.Washburn:
                    return District.Three;
                case County.Adams:
                case County.Clark:
                case County.Columbia:
                case County.Crawford:
                case County.Dane:
                case County.Dodge:
                case County.Grant:
                case County.Green:
                case County.Iowa:
                case County.Jackson:
                case County.Jefferson:
                case County.Juneau:
                case County.LaCrosse:
                case County.Lafayette:
                case County.Marquette:
                case County.Monroe:
                case County.Portage:
                case County.Richland:
                case County.Rock:
                case County.Sauk:
                case County.Vernon:
                case County.Waupaca:
                case County.Waushara:
                case County.Wood:
                    return District.Four;
                default:
                    throw new InvalidEnumArgumentException(nameof(county), (int)county, typeof(County));
            }
        }
    }
}