using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class ReplyBriefDto : IUserData
    {
        [Key]
        [ForeignKey(nameof(BriefDto))]
        public Guid BriefId { get; set; }
        public Guid ApplicationUserId { get; set; }

        public BriefDto BriefDto { get; set; }
    }
}
