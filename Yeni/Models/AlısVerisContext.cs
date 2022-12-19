using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yeni.Models
{
    public partial class AlısVerisContext : DbContext
    {
        public AlısVerisContext()
        {
        }

            public AlısVerisContext(DbContextOptions<AlısVerisContext> options)
                : base(options)
            {
            }

        public virtual DbSet<Kategoriler> Kategorilers { get; set; } = null!;
        public virtual DbSet<Kullaniciler> Kullanicilers { get; set; } = null!;
        public virtual DbSet<SatisDetaylari> SatisDetaylaris { get; set; } = null!;
        public virtual DbSet<Satisler> Satislers { get; set; } = null!;
        public virtual DbSet<Urunler> Urunlers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.;Database=AlısVeris;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategoriler>(entity =>
            {
                entity.HasKey(e => e.KategoriId);

                entity.ToTable("Kategoriler");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.KategoriAdi).HasMaxLength(20000);

                entity.Property(e => e.KategoriResmi).HasMaxLength(10000);
            });

            modelBuilder.Entity<Kullaniciler>(entity =>
            {
                entity.HasKey(e => e.KullaniciId);

                entity.ToTable("Kullaniciler");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.Property(e => e.Adi).HasMaxLength(20);

                entity.Property(e => e.Eposta).HasMaxLength(30);

                entity.Property(e => e.Sifre).HasMaxLength(30);

                entity.Property(e => e.Soyadi).HasMaxLength(20);
            });

            modelBuilder.Entity<SatisDetaylari>(entity =>
            {
                entity.HasKey(e => new { e.SatisId, e.UrunId });

                entity.ToTable("SatisDetaylari");

                entity.Property(e => e.SatisId).HasColumnName("SatisID");

                entity.Property(e => e.UrunId).HasColumnName("UrunID");

                entity.HasOne(d => d.Satis)
                    .WithMany(p => p.SatisDetaylaris)
                    .HasForeignKey(d => d.SatisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SatisDetaylari_Satisler");

                entity.HasOne(d => d.Urun)
                    .WithMany(p => p.SatisDetaylaris)
                    .HasForeignKey(d => d.UrunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SatisDetaylari_Urunler");


            });

            modelBuilder.Entity<Satisler>(entity =>
            {
                entity.HasKey(e => e.SatisId);

                entity.ToTable("Satisler");

                entity.Property(e => e.SatisId).HasColumnName("SatisID");

                entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Satislers)
                    .HasForeignKey(d => d.KullaniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Satisler_Kullaniciler");
                entity.Property(e => e.SatisAdi)
                  .HasMaxLength(20);
                entity.Property(e => e.AlisVerisKontrol)
                .HasDefaultValue(true);

            });

            modelBuilder.Entity<Urunler>(entity =>
            {
                entity.HasKey(e => e.UrunId);

                entity.ToTable("Urunler");

                entity.Property(e => e.UrunId).HasColumnName("UrunID");

                entity.Property(e => e.KategoriId).HasColumnName("KategoriID");

                entity.Property(e => e.UrunAciklamasi).HasMaxLength(2000);

                entity.Property(e => e.UrunAdi).HasMaxLength(20);

                entity.Property(e => e.UrunResmi).HasMaxLength(50000);

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Urunlers)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Urunler_Kategoriler");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
