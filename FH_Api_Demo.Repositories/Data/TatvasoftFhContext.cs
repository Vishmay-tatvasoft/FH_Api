using System;
using System.Collections.Generic;
using FH_Api_Demo.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Data;

public partial class TatvasoftFhContext : DbContext
{
    public TatvasoftFhContext()
    {
    }

    public TatvasoftFhContext(DbContextOptions<TatvasoftFhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FhSystem> FhSystems { get; set; }

    public virtual DbSet<FhUser> FhUsers { get; set; }

    public virtual DbSet<JwField> JwFields { get; set; }

    public virtual DbSet<JwItem> JwItems { get; set; }

    public virtual DbSet<JwList> JwLists { get; set; }

    public virtual DbSet<JwQuery> JwQueries { get; set; }

    public virtual DbSet<JwTable> JwTables { get; set; }

    public virtual DbSet<JwZone> JwZones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=172.16.10.23;Database=Tatvasoft_FH;Username=parthesh;password=parthesh");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FhUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("fh_user_pkey");

            entity.Property(e => e.ActiveUserYn).HasDefaultValueSql("'Y'::character varying");
            entity.Property(e => e.UserType).HasDefaultValueSql("'U'::character varying");
        });

        modelBuilder.Entity<JwField>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("jw_field_pkey");

            entity.Property(e => e.AddDate).HasDefaultValueSql("now()");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.CheckoutFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.DisplayRuleFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");

            entity.HasOne(d => d.List).WithMany(p => p.JwFields).HasConstraintName("list_id");

            entity.HasOne(d => d.Table).WithMany(p => p.JwFields).HasConstraintName("table_id");
        });

        modelBuilder.Entity<JwItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("pk_jw_item");

            entity.Property(e => e.AddDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.DefaultEditFlag).HasDefaultValueSql("'Y'::character varying");
            entity.Property(e => e.DefaultViewFlag).HasDefaultValueSql("'Y'::character varying");
            entity.Property(e => e.PageAccessFlag).HasDefaultValueSql("'V'::character varying");
            entity.Property(e => e.Pos).HasDefaultValueSql("1");
            entity.Property(e => e.Row).HasDefaultValueSql("1");
            entity.Property(e => e.Section).HasDefaultValueSql("1");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.VorEditFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.VorViewFlag).HasDefaultValueSql("'Y'::character varying");
            entity.Property(e => e.WorkbenchFlag).HasDefaultValueSql("'N'::character varying");

            entity.HasOne(d => d.Field).WithMany(p => p.JwItems).HasConstraintName("fk_jw_item_field_id");

            entity.HasOne(d => d.List).WithMany(p => p.JwItems).HasConstraintName("fk_jw_item_list_id");

            entity.HasOne(d => d.Query).WithMany(p => p.JwItems).HasConstraintName("fk_jw_item_query_id");

            entity.HasOne(d => d.Zone).WithMany(p => p.JwItems).HasConstraintName("fk_jw_item_zone_id");
        });

        modelBuilder.Entity<JwList>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("jw_list_pkey");

            entity.Property(e => e.AddDate).HasDefaultValueSql("now()");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.CheckoutFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");
        });

        modelBuilder.Entity<JwQuery>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("pk_jw_query");

            entity.Property(e => e.AddDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.CheckoutFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.DisplayCount).HasDefaultValue(12);
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.UseType).HasDefaultValueSql("'Z'::character varying");
        });

        modelBuilder.Entity<JwTable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("jw_table_pkey");

            entity.Property(e => e.AddDate).HasDefaultValueSql("now()");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.CheckoutFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("now()");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");
        });

        modelBuilder.Entity<JwZone>(entity =>
        {
            entity.HasKey(e => e.ZoneId).HasName("pk_jw_zone");

            entity.Property(e => e.AddDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.AddUser).HasDefaultValueSql("'FH'::character varying");
            entity.Property(e => e.CheckoutFlag).HasDefaultValueSql("'N'::character varying");
            entity.Property(e => e.UpdateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdateUser).HasDefaultValueSql("'FH'::character varying");

            entity.HasOne(d => d.Query).WithMany(p => p.JwZones).HasConstraintName("fk_jw_zone_query_id");

            entity.HasOne(d => d.Table).WithMany(p => p.JwZones).HasConstraintName("fk_jw_zone_table_id");
        });
        modelBuilder.HasSequence("user_suffix_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
