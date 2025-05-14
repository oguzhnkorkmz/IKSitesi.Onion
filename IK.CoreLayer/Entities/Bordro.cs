using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class Bordro : IEntity
    {
        public int BordroID { get; set; }
        public int PersonelID { get; set; }
        public Personel Personel { get; set; }

        public decimal AylikMaaş { get; set; }
        public decimal Kesintiler { get; set; }
        public decimal EkOdeme { get; set; }
        public decimal NetMaaş { get; set; }

        public DateTime BordroTarihi { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }
        public KayitDurumu? KayitDurumu { get; set; }
    }
}
