using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class CircuitCourtCaseDto : IUserData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public County County { get; set; }
        public string CaseNumber { get; set; }
        public Role Role { get; set; }
        public string JudgeFirstName { get; set; }
        public string JudgeLastName { get; set; }
        public string OpponentName { get; set; }
        public string ClientName { get; set; }

        public IList<BriefDto> BriefDto { get; set; }
    }
}
