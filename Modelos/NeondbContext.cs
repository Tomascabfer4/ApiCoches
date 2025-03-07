using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_COCHES.Modelos;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coche> Coches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-orange-math-a4grwjev-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_2jCYiBRbhA1o");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coche>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coche_pkey");

            entity.ToTable("coche");

            entity.HasIndex(e => e.Matricula, "coche_matricula_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Combustible)
                .HasMaxLength(20)
                .HasColumnName("combustible");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Foto).HasColumnName("foto");
            entity.Property(e => e.Kilometros).HasColumnName("kilometros");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Matricula)
                .HasMaxLength(20)
                .HasColumnName("matricula");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .HasColumnName("modelo");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
