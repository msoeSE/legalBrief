using System;

namespace BriefAssistant.Data
{
    public interface IUserData
    {
        Guid ApplicationUserId { get; set; }
    }
}