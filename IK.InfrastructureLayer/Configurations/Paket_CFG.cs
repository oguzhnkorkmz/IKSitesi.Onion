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
    public class Paket_CFG: BaseConfiguration<Paket>, IEntityTypeConfiguration<Paket>
    {
        public  void Configure(EntityTypeBuilder<Paket> builder)
        {
            base.Configure(builder);

        

            // Paket Adı zorunlu, max uzunluğu 100
            builder.Property(p => p.PaketAdi)
                   .IsRequired()
                   .HasMaxLength(100);

            // Fiyat
            builder.Property(p => p.Fiyat)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            // Kapasite Sayısı
            builder.Property(p => p.KapasiteSayisi)
                   .IsRequired();

            builder.Property(b => b.EklenmeTarihi)
              .HasColumnType("smalldatetime")
              .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.KayitDurumu)
                 .HasDefaultValue(KayitDurumu.KayitEkleme);

            // Seed Data (Başlangıç Verileri)
            builder.HasData(
                new Paket
                {
                    PaketID = 1,
                    PaketAdi = "Temel Paket",
                    Fiyat = 199.99m,
                    KapasiteSayisi = 50,
                    EklenmeTarihi = DateTime.UtcNow, // UTC Zamanı kullanmak daha güvenilir
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Paket
                {
                    PaketID = 2,
                    PaketAdi = "Standart Paket",
                    Fiyat = 399.99m,
                    KapasiteSayisi = 150,
                    EklenmeTarihi = DateTime.UtcNow, // UTC Zamanı kullanmak daha güvenilir
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Paket
                {
                    PaketID = 3,
                    PaketAdi = "Premium Paket",
                    Fiyat = 699.99m,
                    KapasiteSayisi = 300,
                    EklenmeTarihi = DateTime.UtcNow, // UTC Zamanı kullanmak daha güvenilir
                    KayitDurumu = KayitDurumu.KayitEkleme
                }
            );
        }
    }
}
