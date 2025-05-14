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
    public class Bolum_CFG : BaseConfiguration<Bolum>, IEntityTypeConfiguration<Bolum>
    {
        public void Configure(EntityTypeBuilder<Bolum> builder)
        {
            base.Configure(builder);

            // Alan özellikleri
            builder.Property(b => b.BolumAdi)
                   .IsRequired()
                   .HasMaxLength(100);

            // Başlangıç verileri
            builder.HasData(
                new Bolum
                {
                    BolumID = 1,
                    BolumAdi = "İnsan Kaynakları",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 2,
                    BolumAdi = "Bilgi Teknolojileri",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 3,
                    BolumAdi = "Muhasebe",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 4,
                    BolumAdi = "Pazarlama",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 5,
                    BolumAdi = "Hukuk",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 6,
                    BolumAdi = "Satın Alma",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                },
                new Bolum
                {
                    BolumID = 7,
                    BolumAdi = "Operasyon",
                    EklenmeTarihi = DateTime.UtcNow,
                    KayitDurumu = KayitDurumu.KayitEkleme
                });
        }
    }
}
