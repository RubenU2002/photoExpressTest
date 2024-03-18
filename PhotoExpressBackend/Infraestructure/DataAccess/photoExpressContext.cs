using System;
using System.Collections.Generic;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.DataAccess;

public partial class photoExpressContext : DbContext
{
    public photoExpressContext()
    {
    }

    public photoExpressContext(DbContextOptions<photoExpressContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventModificationLog> EventModificationLogs { get; set; }

    public virtual DbSet<HigherEducationInstitution> HigherEducationInstitutions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VR0JLRP\\RUBENURREGOBD;Database=photoExpressDB;User Id=sa;Password=081019; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C87011B90D47");

            entity.ToTable("Event");

            entity.Property(e => e.EventId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EventID");
            entity.Property(e => e.CapAndGown).HasColumnName("capAndGown");
            entity.Property(e => e.InstitutionId).HasColumnName("InstitutionID");
            entity.Property(e => e.ServiceCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Institution).WithMany(p => p.Events)
                .HasForeignKey(d => d.InstitutionId)
                .HasConstraintName("FK__Event__Instituti__286302EC");
        });

        modelBuilder.Entity<EventModificationLog>(entity =>
        {
            entity.HasKey(e => e.ModificationId).HasName("PK__EventMod__A3FE5A1266D45265");

            entity.ToTable("EventModificationLog");

            entity.Property(e => e.ModificationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ModificationID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Event).WithMany(p => p.EventModificationLogs)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventModi__Event__2C3393D0");
        });

        modelBuilder.Entity<HigherEducationInstitution>(entity =>
        {
            entity.HasKey(e => e.InstitutionId).HasName("PK__HigherEd__8DF6B94D8D7EBB93");

            entity.ToTable("HigherEducationInstitution");

            entity.Property(e => e.InstitutionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("InstitutionID");
            entity.Property(e => e.InstitutionAddress).HasMaxLength(255);
            entity.Property(e => e.InstitutionName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
