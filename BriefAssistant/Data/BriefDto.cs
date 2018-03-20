using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BriefAssistant.Data
{
    public class BriefDto : IUserData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        [ForeignKey(nameof(CircuitCourtCaseDto))]
        public Guid CircuitCourtCaseId { get; set; }
        [ForeignKey(nameof(ContactInfoDto))]
        public Guid ContactInfoId { get; set; }
        
        public Models.BriefType Type { get; set; }

        public string Title { get; set; }
        public string AppellateCourtCaseNumber { get; set; }
        public string Argument { get; set; }
        public string Conclusion { get; set; }

        public ContactInfoDto ContactInfoDto { get; set; }
        public CircuitCourtCaseDto CircuitCourtCaseDto { get; set; }
    }
}
