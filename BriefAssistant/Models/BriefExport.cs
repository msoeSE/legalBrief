using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    [DataContract(Namespace = "")]
    public class BriefExport
    {
        [DataContract(Namespace = "")]
        public class Paragraph
        {
            public Paragraph(string value)
            {
                Value = value;
            }

            [DataMember]
            public string Value { get; set; }
        }

        [DataMember]
        public District District { get; set; }
        [DataMember]
        public ContactInfo ContactInfo { get; set; }
        [DataMember]
        public CircuitCourtCase CircuitCourtCase { get; set; }
        [DataMember]
        public string AppellateCourtCaseNumber { get; set; }
        [DataMember]
        public IList<Paragraph> IssuesPresented { get; set; }
        [DataMember]
        public IList<Paragraph> OralArgumentStatement { get; set; }
        [DataMember]
        public IList<Paragraph> PublicationStatement { get; set; }
        [DataMember]
        public IList<Paragraph> CaseFactsStatement { get; set; }
        [DataMember]
        public IList<Paragraph> Argument { get; set; }
        [DataMember]
        public IList<Paragraph> Conclusion { get; set; }
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
            Argument = ToParagraphs(info.Argument);
            Conclusion = ToParagraphs(info.Conclusion);
            SetTopAndBottomNamesAndRoles(CircuitCourtCase.Role, info.Type);
            District = GetDistrictFromCounty(CircuitCourtCase.County);
        }

        /// <summary>
        /// Sets the information required in an initial brief
        /// </summary>
        /// <param name="info">
        /// The object containing the necessary information for an initial brief
        /// </param>
        public void SetInitialInformation(InitialBriefInfo info)
        {
            IssuesPresented = ToParagraphs(info.IssuesPresented);
            OralArgumentStatement = ToParagraphs(info.OralArgumentStatement);
            PublicationStatement = ToParagraphs(info.PublicationStatement);
            CaseFactsStatement = ToParagraphs(info.CaseFactsStatement);
            AppendixDocuments = info.AppendixDocuments;
        }

        private static IList<Paragraph> ToParagraphs(string s)
        {
            return s.Trim()
                .Split('\n')
                .Select(p => new Paragraph(p))
                .ToList();
        }

        /// <summary>
        /// Sets the information required in an reply brief
        /// </summary>
        /// <param name="info">
        /// The object containing the necessary information for an reply brief
        /// </param>
        public void SetReplyInformation(ReplyBriefInfo info)
        {

        }

        /// <summary>
        /// Sets the information required in an response brief
        /// </summary>
        /// <param name="info">
        /// The object containing the necessary information for an response brief
        /// </param>
        public void SetResponseInformation(ResponseBriefInfo info)
        {
            IssuesPresented = ToParagraphs(info.IssuesPresented);
            OralArgumentStatement = ToParagraphs(info.OralArgumentStatement);
            PublicationStatement = ToParagraphs(info.PublicationStatement);
            CaseFactsStatement = ToParagraphs(info.CaseFactsStatement);
        }

        /// <summary>
        /// This method determines what order the appellant and opponent name and roles appear on the brief
        /// </summary>
        /// <param name="role">The role of the appellant</param>
        /// <param name="type">The type of brief being generated</param>
        private void SetTopAndBottomNamesAndRoles(Role role, BriefType type)
        {
            switch (role)
            {
                case Role.Plaintiff:
                    TopName = ContactInfo.Name;
                    BottomName = CircuitCourtCase.OpponentName;
                    switch (type)
                    {
                        case BriefType.Response:
                            TopRole = "Plaintiff-Respondent";
                            BottomRole = "Defendant-Appellant";
                            break;
                        default:
                            TopRole = "Plaintiff-Appellant";
                            BottomRole = "Defendant-Respondent";
                            break;
                    }
                    
                    break;
                case Role.Petitioner:
                    TopName = ContactInfo.Name;
                    BottomName = CircuitCourtCase.OpponentName;
                    switch (type)
                    {
                        case BriefType.Response:
                            TopRole = "Petitioner-Respondent";
                            BottomRole = "Respondent-Appellant";
                            break;
                        default:
                            TopRole = "Petitioner-Appellant";
                            BottomRole = "Respondent-Respondent";
                            break;
                    }
                    
                    break;
                case Role.Defendent:
                    TopName = CircuitCourtCase.OpponentName;
                    BottomName = ContactInfo.Name;
                    switch (type)
                    {
                        case BriefType.Response:
                            TopRole = "Plaintiff-Appellant";
                            BottomRole = "Defendant-Respondent";
                            break;
                        default:
                            TopRole = "Plaintiff-Respondent";
                            BottomRole = "Defendant-Appellant";
                            break;
                    }

                    break;
                case Role.Respondent:
                    TopName = CircuitCourtCase.OpponentName;
                    BottomName = ContactInfo.Name;
                    switch (type)
                    {
                        case BriefType.Response:
                            TopRole = "Petitioner-Appellant";
                            BottomRole = "Respondent-Respondent";
                            break;
                        default:
                            TopRole = "Petitioner-Respondent";
                            BottomRole = "Respondent-Appellant";
                            break;
                    }
                    
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(role), (int)role, typeof(Role));
            }
        }

        /// <summary>
        /// This method determines which district to put on the brief based on the county of the circuit court case
        /// </summary>
        /// <param name="county">The county that the circuit court case took place in</param>
        /// <returns>The district type of the county</returns>
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