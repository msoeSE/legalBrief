using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BriefAssistant.Models;

namespace BriefAssistant
{
    public partial class Brief_assistantContext : DbContext
    {
        public Brief_assistantContext(DbContextOptions<Brief_assistantContext> options) : base(options) { }
        public virtual DbSet<BriefInfo> BriefInfo { get; set; }
        public virtual DbSet<CircuitCourtCase> CaseInfo { get; set; }
        public virtual DbSet<Appellant> UserInfo { get; set; }
        public virtual DbSet<Address> Address { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=brief_assistant;Username=postgres;Password=P823kmc123");
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

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("address", "brief");


                entity.Property(e => e.Street).HasColumnName("street");

                entity.Property(e => e.Street2).HasColumnName("Street2");

                entity.Property(e => e.Zip).HasColumnName("zip");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.City).HasColumnName("City");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Appellant)
                                   .WithOne(p => p.Address)
                                   .HasForeignKey<Address>(d => d.UserId)
                                   .OnDelete(DeleteBehavior.ClientSetNull)
                                   .HasConstraintName("constraint_fkey");
            });
            modelBuilder.Entity<BriefInfo>(entity =>
            {
                entity.HasKey(e => e.InitialBriefId);

                entity.ToTable("brief_info", "brief");

                entity.HasIndex(e => e.CaseId)
                    .HasName("constraint_brief_ukey")
                    .IsUnique();

                entity.Property(e => e.InitialBriefId)
                    .HasColumnName("initial_brief_id")
                    .HasDefaultValueSql("nextval('brief.brief_info_initial_brief_id_seq'::regclass)");

                entity.Property(e => e.AppendexDocuments).HasColumnName("appendex_documents");

                entity.Property(e => e.Argument).HasColumnName("argument");

                entity.Property(e => e.CaseFactsStatement).HasColumnName("case_facts_statement");

                entity.Property(e => e.CaseId).HasColumnName("case_id");

                entity.Property(e => e.Conclusion).HasColumnName("conclusion");

                entity.Property(e => e.IssuesPresented).HasColumnName("issues_presented");

                entity.Property(e => e.OralArgumentStatement).HasColumnName("oral_argument_statement");

                entity.Property(e => e.PublicationStatement).HasColumnName("publication_statement");

                entity.HasOne(d => d.CircuitCourtCase)
                    .WithOne(p => p.BriefInfo)
                    .HasForeignKey<BriefInfo>(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("constraint_fkey");

                entity.HasOne(d =>  d.Appellant)
                    .WithOne(p => p.BriefInfo)
                    .HasForeignKey<BriefInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("constraint_fkey_user");
            });

            modelBuilder.Entity<CircuitCourtCase>(entity =>
            {
                entity.HasKey(e => e.CaseId);

                entity.ToTable("case_info", "brief");

                entity.HasIndex(e => e.UserId)
                    .HasName("constraint_ukey")
                    .IsUnique();

                entity.Property(e => e.CaseId)
                    .HasColumnName("case_id")
                    .HasDefaultValueSql("nextval('brief.case_info_case_id_seq'::regclass)");

                entity.Property(e => e.CaseNumber).HasColumnName("case_no");

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.JudgeFirstName).HasColumnName("judge_first_name");

                entity.Property(e => e.JudgeLastName).HasColumnName("judge_last_name");

                entity.Property(e => e.OpponentName).HasColumnName("opponent_name");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Appellant)
                    .WithOne(p => p.CircuitCourtCase)
                    .HasForeignKey<CircuitCourtCase>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("constraint_fkey");
            });

            modelBuilder.Entity<Appellant>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("user_info", "brief");

                entity.HasIndex(e => e.Email)
                    .HasName("user_info_email_key")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("nextval('brief.user_info_user_id_seq'::regclass)");



                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Phone).HasColumnName("phone");


            });

        }
    }
}
