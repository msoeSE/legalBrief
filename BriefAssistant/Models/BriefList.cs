using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class BriefList
    {
        public IEnumerable<BriefListItem> Briefs { get; set; }
    }
}
