using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IK.CoreLayer.Entities
{
    public class Kurum:IEntity
    {
        public int KurumID { get; set; }
        public string KurumAdi { get; set; }
        public string Adres { get; set; }

        public int PaketID { get; set; }
        public Paket? Paket { get; set; }

        public DateTime PaketBaslangicTarihi { get; set; }
        public DateTime PaketBitisTarihi { get; set; }

        public bool PaketAktifMi { get; set; }

        public ICollection<Personel>? Personeller { get; set; }
        public  string  VergiNumarasi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }

        public KayitDurumu? KayitDurumu { get; set; }

        public ICollection<ApplicationUser>? KullaniciHesaplari { get; set; } 
    }
}
