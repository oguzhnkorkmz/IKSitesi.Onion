using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IK.InfrastructureLayer.Configurations
{
    public class HarcamaTalebi_CFG : BaseConfiguration<HarcamaTalebi>, IEntityTypeConfiguration<HarcamaTalebi>
    {
        public void Configure(EntityTypeBuilder<HarcamaTalebi> builder)
        {
            // BaseConfiguration'dan gelen genel ayarları uygula
            base.Configure(builder);

            // HarcamaTalebiID anahtar olarak belirlenir
            builder.HasKey(ht => ht.HarcamaTalebiID);

            // Personel ile ilişki
            builder.HasOne(ht => ht.Personel)
                   .WithMany(p => p.HarcamaTalepleri)
                   .HasForeignKey(ht => ht.PersonelID)
                   .OnDelete(DeleteBehavior.Restrict);  // Personel silindiğinde harcama talebi verisi kaybolmasın

            // Harcama Tutarı zorunlu, decimal formatında
            builder.Property(ht => ht.HarcamaTutari)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Açıklama
            builder.Property(ht => ht.Aciklama)
                   .HasMaxLength(500);  // Maksimum açıklama uzunluğu

            // Onay Durumu (Onaylı mı değil mi) alanını zorunlu yapıyoruz.
            builder.Property(ht => ht.Onaylimi)
                   .IsRequired();

            // KayitDurumu'nu zorunlu yapıyoruz.
            builder.Property(ht => ht.KayitDurumu)
                   .IsRequired();

            // Eklenme, güncelleme ve silme tarihleri
            builder.Property(ht => ht.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .HasDefaultValueSql("GETDATE()");  // Varsayılan olarak güncel tarih ekle

            builder.Property(ht => ht.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(ht => ht.SilmeTarihi)
                   .HasColumnType("smalldatetime");
        }
    }
}
