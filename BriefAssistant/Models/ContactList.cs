using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class ContactList
    {
        public IEnumerable<ContactInfo> Contacts { get; set; }
    }
}
