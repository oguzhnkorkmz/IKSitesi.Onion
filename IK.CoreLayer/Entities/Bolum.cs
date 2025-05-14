using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class Bolum:IEntity
    {
        public int BolumID { get; set; }
        public string BolumAdi { get; set; }

        public ICollection<Personel>? Personeller { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.UtcNow;

        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }

        public KayitDurumu? KayitDurumu { get; set; }

       
    }
}
