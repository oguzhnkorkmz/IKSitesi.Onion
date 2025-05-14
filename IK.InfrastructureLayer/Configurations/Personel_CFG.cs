using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Configurations
{
    public class Personel_CFG : BaseConfiguration<Personel>, IEntityTypeConfiguration<Personel>
    {
        public void Configure(EntityTypeBuilder<Personel> builder)
        {
            // BaseConfiguration'dan gelen genel ayarları uygula
            base.Configure(builder);

            // Zorunlu alanlar
            builder.Property(p => p.PersonelAdi)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.PersonelSoyadi)
                   .IsRequired()
                   .HasMaxLength(50);

            // Kurum ile ilişki
            builder.HasOne(p => p.Kurum)
                   .WithMany(k => k.Personeller)
                   .HasForeignKey(p => p.KurumID)
                   .OnDelete(DeleteBehavior.Restrict);  // Kurum silinirse personel verisi kaybolmasın

            // Bölüm ile ilişki
            builder.HasOne(p => p.Bolum)
                   .WithMany(b => b.Personeller)
                   .HasForeignKey(p => p.BolumID)
                   .OnDelete(DeleteBehavior.Restrict);  // Bölüm silinirse personel verisi kaybolmasın

            // Kullanıcı hesabı ile ilişki (Opsiyonel)
            builder.HasOne(p => p.KullaniciHesabi)
                   .WithOne(u => u.Personel)
                   .HasForeignKey<Personel>(p => p.KullaniciHesabiID)
                   .OnDelete(DeleteBehavior.SetNull);  // Kullanıcı silindiğinde personel kaydının silinmesini istemiyorsanız

            // Seed data eklememek için herhangi bir işlem yapılmadı.
        }
    }
}

