using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace v_conf_net.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlternateComponentMaster> AlternateComponentMasters { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<SgMfgMaster> SgMfgMasters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VehicleDefaultConfig> VehicleDefaultConfigs { get; set; }

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=v_conf;user=root;password=aditi", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.43-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AlternateComponentMaster>(entity =>
        {
            entity.HasKey(e => e.AltId).HasName("PRIMARY");

            entity.HasOne(d => d.AltComp).WithMany(p => p.AlternateComponentMasterAltComps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("acm_fk_alt_comp");

            entity.HasOne(d => d.Comp).WithMany(p => p.AlternateComponentMasterComps)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("acm_fk_comp");

            entity.HasOne(d => d.Model).WithMany(p => p.AlternateComponentMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("acm_fk_model");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.CompId).HasName("PRIMARY");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvDtlId).HasName("PRIMARY");

            entity.HasOne(d => d.Comp).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_fk_component");

            entity.HasOne(d => d.Inv).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_fk_invoice");
        });

        modelBuilder.Entity<InvoiceHeader>(entity =>
        {
            entity.HasKey(e => e.InvId).HasName("PRIMARY");

            entity.HasOne(d => d.Model).WithMany(p => p.InvoiceHeaders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ih_fk_model");

            entity.HasOne(d => d.User).WithMany(p => p.InvoiceHeaders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ih_fk_user");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.MfgId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PRIMARY");

            entity.HasOne(d => d.Mfg).WithMany(p => p.Models).HasConstraintName("model_ibfk_1");

            entity.HasOne(d => d.Seg).WithMany(p => p.Models).HasConstraintName("model_ibfk_2");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.SegId).HasName("PRIMARY");
        });

        modelBuilder.Entity<SgMfgMaster>(entity =>
        {
            entity.HasKey(e => e.SgmfId).HasName("PRIMARY");

            entity.HasOne(d => d.Mfg).WithMany(p => p.SgMfgMasters).HasConstraintName("sg_mfg_master_ibfk_1");

            entity.HasOne(d => d.Seg).WithMany(p => p.SgMfgMasters).HasConstraintName("sg_mfg_master_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.FailedAttempts).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsBlocked).HasDefaultValueSql("b'0'");
        });

        modelBuilder.Entity<VehicleDefaultConfig>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PRIMARY");

            entity.HasOne(d => d.Comp).WithMany(p => p.VehicleDefaultConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vdc_fk_component");

            entity.HasOne(d => d.Model).WithMany(p => p.VehicleDefaultConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vdc_fk_model");
        });

        modelBuilder.Entity<VehicleDetail>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PRIMARY");

            entity.HasOne(d => d.Comp).WithMany(p => p.VehicleDetails).HasConstraintName("vd_fk_comp");

            entity.HasOne(d => d.Model).WithMany(p => p.VehicleDetails).HasConstraintName("vd_fk_model");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
