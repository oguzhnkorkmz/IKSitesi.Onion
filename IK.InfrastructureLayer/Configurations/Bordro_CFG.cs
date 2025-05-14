using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IK.InfrastructureLayer.Configurations
{
    public class Bordro_CFG : BaseConfiguration<Bordro>, IEntityTypeConfiguration<Bordro>
    {
        public void Configure(EntityTypeBuilder<Bordro> builder)
        {
            // BaseConfiguration'dan gelen genel ayarları uygula
            base.Configure(builder);

            // BordroID anahtar olarak belirlenir
            builder.HasKey(b => b.BordroID);

            // Personel ile ilişki
            builder.HasOne(b => b.Personel)
                   .WithMany(p => p.Bordrolar)
                   .HasForeignKey(b => b.PersonelID)
                   .OnDelete(DeleteBehavior.Restrict);  // Personel silindiğinde bordro verisi kaybolmasın

            // Aylık Maaş
            builder.Property(b => b.AylikMaaş)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Kesintiler
            builder.Property(b => b.Kesintiler)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Ek Ödeme
            builder.Property(b => b.EkOdeme)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Net Maaş
            builder.Property(b => b.NetMaaş)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Bordro tarihi
            builder.Property(b => b.BordroTarihi)
                   .IsRequired()
                   .HasColumnType("smalldatetime");

            // KayitDurumu
            builder.Property(b => b.KayitDurumu)
                   .IsRequired();

            // Eklenme, güncelleme ve silme tarihleri
            builder.Property(b => b.EklenmeTarihi)
                   .HasColumnType("smalldatetime")
                   .HasDefaultValueSql("GETDATE()");  // Varsayılan olarak güncel tarih ekle

            builder.Property(b => b.GuncellemeTarihi)
                   .HasColumnType("smalldatetime");

            builder.Property(b => b.SilmeTarihi)
                   .HasColumnType("smalldatetime");
        }
    }
}
