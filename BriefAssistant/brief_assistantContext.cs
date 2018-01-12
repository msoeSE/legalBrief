using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BriefAssistant.Models;

namespace BriefAssistant
{
    public partial class Brief_assistantContext : DbContext
    {
        public Brief_assistantContext(DbContextOptions<Brief_assistantContext> options) : base(options) { }
        public virtual DbSet<DbbriefInfo> BriefInfo { get; set; }
        public virtual DbSet<DbCaseInfo> CaseInfo { get; set; }
        public virtual DbSet<DbUserInfo> UserInfo { get; set; }
        public virtual DbSet<Brief> Brief { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=brief_assistant;Username=postgres;Password=");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence("brief_info_initial_brief_id_seq", schema: "brief")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("case_info_case_id_seq", schema: "brief")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("user_info_user_id_seq", schema: "brief")
                .HasMin(1)
                .HasMax(2147483647);

            
            modelBuilder.Entity<DbbriefInfo>(entity =>
            {
                entity.HasKey(e => e.InitialBriefInfoId);

                entity.ToTable("brief_info", "brief");


                entity.Property(e => e.InitialBriefInfoId)
                    .HasColumnName("initial_brief_id")
                    .HasDefaultValueSql("nextval('brief.brief_info_initial_brief_id_seq'::regclass)");

                entity.Property(e => e.AppendixDocuments).HasColumnName("appendix_documents");

                entity.Property(e => e.AppellateCourtCaseNumber).HasColumnName("appellate_court_case_number");

                entity.Property(e => e.Argument).HasColumnName("argument");

                entity.Property(e => e.CaseFactsStatement).HasColumnName("case_facts_statement");

                entity.Property(e => e.Conclusion).HasColumnName("conclusion");

                entity.Property(e => e.IssuesPresented).HasColumnName("issues_presented");

                entity.Property(e => e.OralArgumentStatement).HasColumnName("oral_argument_statement");

                entity.Property(e => e.PublicationStatement).HasColumnName("publication_statement");

            });

            modelBuilder.Entity<DbCaseInfo>(entity =>
            {
                entity.HasKey(e => e.CaseId);

                entity.ToTable("case_info", "brief");

                entity.Property(e => e.CaseId)
                    .HasColumnName("case_id")
                    .HasDefaultValueSql("nextval('brief.case_info_case_id_seq'::regclass)");

                entity.Property(e => e.CaseNumber).HasColumnName("case_no");

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.JudgeFirstName).HasColumnName("judge_first_name");

                entity.Property(e => e.JudgeLastName).HasColumnName("judge_last_name");

                entity.Property(e => e.OpponentName).HasColumnName("opponent_name");

                entity.Property(e => e.Role).HasColumnName("role");

            });

            modelBuilder.Entity<DbUserInfo>(entity =>
            {
                entity.HasKey(e => e.UserInfoId);

                entity.ToTable("user_info", "brief");

                entity.HasIndex(e => e.Email)
                    .HasName("user_info_email_key")
                    .IsUnique();

                entity.Property(e => e.UserInfoId)
                    .HasColumnName("userinfo_id")
                    .HasDefaultValueSql("nextval('brief.user_info_userinfo_id_seq'::regclass)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasDefaultValue("")
                     .HasColumnName("name");

                entity.Property(e => e.Street2).HasColumnName("street2");

                entity.Property(e => e.Zip).HasColumnName("zip");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.City).HasColumnName("city");
            
                entity.Property(e => e.Phone).HasColumnName("phone");

            });

            modelBuilder.Entity<Brief>(entity =>
            {
                entity.HasKey(e => e.BriefId);

                entity.ToTable("brief", "brief");


                entity.HasIndex(e => e.BriefId)
                    .HasName("constraint_brief_ukey")
                    .IsUnique();

                entity.Property(e => e.BriefId)
                    .HasColumnName("brief_id")
                    .HasDefaultValueSql("nextval('brief.brief_brief_id_seq'::regclass)");

                entity.Property(e => e.UserInfoId)
                    .HasColumnName("user_id");

                entity.Property(e => e.InitialBriefInfoId)
                    .HasColumnName("initial_brief_id");

                entity.Property(e => e.CaseId)
                    .HasColumnName("case_id");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.HasOne(d => d.UserInfo)
                    .WithOne()
                    .HasForeignKey<Brief>(d => d.UserInfoId);

                entity.HasOne(d => d.BriefInfo)
                    .WithOne()
                    .HasForeignKey<Brief>(d => d.InitialBriefInfoId);

                entity.HasOne(d => d.CaseInfo)
                    .WithOne(b => b.Brief)
                    .HasForeignKey<Brief>(d => d.CaseId);

                entity.HasOne(d => d.User)
                    .WithMany(b => b.BriefRecord)
                    .HasForeignKey(d => d.Id);
            });
        }
    }
}
