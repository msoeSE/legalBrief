﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BriefAssistant.Models
{
    public class DbUserInfo
    {
        public int UserId { get; set; }
        public DbCaseInfo CaseInfo { get; set; }
        public DbbriefInfo BriefInfo { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
