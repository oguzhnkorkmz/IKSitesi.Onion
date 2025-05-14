using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class Paket:IEntity
    {
        public int PaketID { get; set; }
        public string PaketAdi { get; set; }

        public decimal Fiyat { get; set; }
        public int PaketSuresi { get; set; }
        public bool AktifMi { get; set; } = true;

        public int KapasiteSayisi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }


        public KayitDurumu? KayitDurumu { get; set; }

        public ICollection<Kurum>? Kurumlar { get; set; }
    }
}
