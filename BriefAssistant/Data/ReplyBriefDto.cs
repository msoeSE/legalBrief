using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BriefAssistant.Data
{
    public class ReplyBriefDto : IUserData
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}
