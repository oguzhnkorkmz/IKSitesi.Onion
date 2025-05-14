using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class Personel : IEntity
    {
        public int PersonelID { get; set; }
        public string PersonelAdi { get; set; }
        public string PersonelSoyadi { get; set; }

        // İlişki: Personelin ait olduğu kurum
        public int KurumID { get; set; }
        public Kurum? Kurum { get; set; }

        // İlişki: Personelin çalıştığı bölüm
        public int BolumID { get; set; }
        public Bolum? Bolum { get; set; }

        // İzin Talep ve Bordro bilgileri gibi ilişkiler
        public ICollection<IzinTalebi>? IzinTalepleri { get; set; }
        public ICollection<AvansTalebi>? AvansTalepleri { get; set; }
        public ICollection<HarcamaTalebi>? HarcamaTalepleri { get; set; }
        public ICollection<Bordro>? Bordrolar { get; set; }

        // Kayıt tarihi ve güncellenme bilgileri
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }

        // Kayıt durumu
        public KayitDurumu? KayitDurumu { get; set; }

        // Kullanıcı Hesabı ile ilişki
        public ApplicationUser? KullaniciHesabi { get; set; }
        public int? KullaniciHesabiID { get; set; }
    }
}
