using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class HarcamaTalebi : IEntity
    {
        public int HarcamaTalebiID { get; set; }
        public int PersonelID { get; set; }
        public Personel? Personel { get; set; }

        public decimal HarcamaTutari { get; set; }
        public string Aciklama { get; set; }
        public bool Onaylimi { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }
        public KayitDurumu? KayitDurumu { get; set; }
    }
}
