using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BriefAssistant.Models
{
    public class InitialBriefHolder
    {
        public InitialBriefInfo InitialBriefInfo { get; set; }
        public BriefInfo BriefInfo { get; set; }
    }
}