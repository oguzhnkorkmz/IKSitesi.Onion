using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IK.InfrastructureLayer.Configurations
{
    public class IzinTalebi_CFG : BaseConfiguration<IzinTalebi>, IEntityTypeConfiguration<IzinTalebi>
    {
        public void Configure(EntityTypeBuilder<IzinTalebi> builder)
        {
            // BaseConfiguration'dan gelen genel ayarları uygula
            base.Configure(builder);

            // IzinTalebiID anahtar olarak belirlenir
            builder.HasKey(it => it.IzinTalebiID);

            // Personel ile ilişki
            builder.HasOne(it => it.Personel)
                   .WithMany(p => p.IzinTalepleri)
                   .HasForeignKey(it => it.PersonelID)
                   .OnDelete(DeleteBehavior.Restrict);  // Personel silindiğinde izin talebi verisi kaybolmasın

            // Başlangıç ve bitiş tarihleri
            builder.Property(it => it.BaslangicTarihi)
                   .IsRequired()
                   .HasColumnType("smalldatetime");

            builder.Property(it => it.BitisTarihi)
                   .IsRequired()
                   .HasColumnType("smalldatetime");

            // Onay Durumu (Onaylı mı değil mi) alanını zorunlu yapıyoruz.
            builder.Property(it => it.Onaylimi)
                   .IsRequired();

            // KayitDurumu'nu zorunlu yapıyoruz.
            builder.Property(it => it.KayitDurumu)
                   .IsRequired();

            // Eklenme, güncelleme ve silme tarihleri
            builder.Property(it => it.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .HasDefaultValueSql("GETDATE()");  // Varsayılan olarak güncel tarih ekle

            builder.Property(it => it.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(it => it.SilmeTarihi)
                   .HasColumnType("smalldatetime");
        }
    }
}
