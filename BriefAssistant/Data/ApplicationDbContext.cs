﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BriefAssistant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<BriefDto> Briefs { get; set; }
        public DbSet<InitialBriefDto> Initials { get; set; }
        public DbSet<ReplyBriefDto> Replies { get; set; }
        public DbSet<ResponseBriefDto> Responses { get; set; }
        public DbSet<CircuitCourtCaseDto> Cases { get; set; }
        public DbSet<ContactInfoDto> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}