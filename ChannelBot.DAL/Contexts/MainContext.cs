using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ChannelBot.DAL.Models;

namespace ChannelBot.DAL.Contexts
{
    public partial class MainContext: DbContext
    {
        private string _connString;
        public MainContext(string connString)
        {
            _connString = connString;
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupSource> GroupSource { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<UserCredential> UserCredential { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('contentcategoryid_seq'::regclass)");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('contentid_seq'::regclass)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.MediaUrl)
                    .IsRequired()
                    .HasColumnName("MediaURL");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Content)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c_csi_fk");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('contentgroupid_seq'::regclass)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("g_cg_fk");
            });

            modelBuilder.Entity<GroupSource>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.SourceId })
                    .HasName("GroupSources_pkey");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupSource)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gs_gi_fk");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.GroupSource)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("gs_si_fk");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('platformid_seq'::regclass)");

                entity.Property(e => e.Url).HasColumnName("URL");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('sontentsourceid_seq'::regclass)");

                entity.Property(e => e.SourceUrl).HasColumnName("SourceURL");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("UserAccount_pkey");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithOne(p => p.UserCredential)
                    .HasForeignKey<UserCredential>(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ua_cci_fk");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.UserCredential)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ua_pi_fk");
            });

            modelBuilder.HasSequence("contentcategoryid_seq");

            modelBuilder.HasSequence("contentgroupid_seq");

            modelBuilder.HasSequence("ContentGroupId_seq");

            modelBuilder.HasSequence("contentid_seq");

            modelBuilder.HasSequence("platformid_seq");

            modelBuilder.HasSequence("sontentsourceid_seq");

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connString);
        }
    }
}
