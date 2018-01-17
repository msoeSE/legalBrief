using System.Collections.Generic;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class ContactInfoDto : IUserData
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public IList<BriefDto> BriefDto { get; set; }
    }
}
