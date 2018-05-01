using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BriefAssistant.Models;

namespace BriefAssistant.Data
{
    public class ContactInfoDto : IUserData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BarId { get; set; }

        public IList<BriefDto> BriefDto { get; set; }
    }
}
