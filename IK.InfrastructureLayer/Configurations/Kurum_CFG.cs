using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection.Emit;

namespace IK.InfrastructureLayer.Configurations
{
    public class Kurum_CFG : BaseConfiguration<Kurum> // BaseConfiguration'i kullanıyoruz
    {
        public override void Configure(EntityTypeBuilder<Kurum> builder)
        {
            base.Configure(builder); // BaseConfiguration'dan gelen ortak ayarları alıyoruz.

            // Kurum Adı
            builder.Property(k => k.KurumAdi)
                   .IsRequired()
                   .HasMaxLength(100);

            // Adres
            builder.Property(k => k.Adres)
                   .HasMaxLength(250);

            // Vergi numarası
            builder.Property(k => k.VergiNumarasi)
                   .IsRequired()
                   .HasMaxLength(15);

            // Paket
            builder.HasOne(k => k.Paket)
           .WithMany(p => p.Kurumlar) // Paket içinde ICollection<Kurum> olmalı
           .HasForeignKey(k => k.PaketID)
           .OnDelete(DeleteBehavior.Restrict);


            // Paket başlangıç ve bitiş tarihleri
            builder.Property(k => k.PaketBaslangicTarihi)
                   .IsRequired()
                   .HasColumnType("smalldatetime");

            builder.Property(k => k.PaketBitisTarihi)
                   .IsRequired()
                   .HasColumnType("smalldatetime");

            // Paket durumu
            builder.Property(k => k.PaketAktifMi)
                   .IsRequired();

            // Kayit durumu
            builder.Property(k => k.KayitDurumu)
                   .IsRequired();

            // Eklenme, güncelleme ve silme tarihleri
            builder.Property(k => k.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .IsRequired();

            builder.Property(k => k.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(k => k.SilmeTarihi)
                   .HasColumnType("smalldatetime");

            // Kurum için ilişkiler
            builder.HasMany(k => k.Personeller)
                   .WithOne(p => p.Kurum)
                   .HasForeignKey(p => p.KurumID)
                   .OnDelete(DeleteBehavior.Cascade); // Personel silindiğinde Kurum'u etkilemesin

            builder.HasMany(k => k.KullaniciHesaplari)
                   .WithOne(u => u.Kurum)
                   .HasForeignKey(u => u.KurumID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                 new Kurum
                 {
                         KurumID = 1,
                         KurumAdi = "Site Admin Kurumu",
                         Adres = "Henüz Belirtilmedi",
                         VergiNumarasi = "0000000000",
                         PaketID = 1, // Paket tablosunda 1 ID'li bir paket ekli olmalı
                         PaketBaslangicTarihi = new DateTime(2025, 4, 26),
                         PaketBitisTarihi = new DateTime(2026, 4, 26),
                         PaketAktifMi = true,
                         KayitDurumu = KayitDurumu.KayitEkleme,
                         EklenmeTarihi = new DateTime(2025, 4, 26),
                         GuncellemeTarihi = null,
                         SilmeTarihi = null 
                 }
            );



        }
    }
}
