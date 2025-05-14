using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace IK.InfrastructureLayer.Configurations
{
    public class AvansTalebi_CFG : BaseConfiguration<AvansTalebi> // BaseConfiguration'i kullanıyoruz
    {
        public override void Configure(EntityTypeBuilder<AvansTalebi> builder)
        {
            base.Configure(builder); // BaseConfiguration'dan gelen ortak ayarları alıyoruz.

            // Avans Talebi ID'yi tanımlıyoruz.
            builder.HasKey(at => at.AvansTalebiID);

            // Personel ile ilişkiyi tanımlıyoruz.
            builder.HasOne(at => at.Personel)
                   .WithMany(p => p.AvansTalepleri)  // Bir personelin birden fazla avans talebi olabilir.
                   .HasForeignKey(at => at.PersonelID)
                   .OnDelete(DeleteBehavior.Restrict); // Personel silindiğinde avans talepleri silinmesin.

            // Talep Edilen Tutar alanını zorunlu yapıyoruz.
            builder.Property(at => at.TalepEdilenTutar)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");  // Decimalleri uygun şekilde tanımlıyoruz.

            // Onay Durumu (Onaylı mı değil mi) alanını zorunlu yapıyoruz.
            builder.Property(at => at.Onaylimi)
                   .IsRequired();

            // KayitDurumu'nu zorunlu yapıyoruz.
            builder.Property(at => at.KayitDurumu)
                   .IsRequired();

            // Eklenme Tarihi'ni ve diğer tarih alanlarını tanımlıyoruz.
            builder.Property(at => at.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .HasDefaultValueSql("GETDATE()");  // Varsayılan olarak güncel tarih ekle.

            builder.Property(at => at.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(at => at.SilmeTarihi)
                   .HasColumnType("smalldatetime");
        }
    }
}
