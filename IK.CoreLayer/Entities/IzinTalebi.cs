using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Entities
{
    public class IzinTalebi : IEntity
    {
        public int IzinTalebiID { get; set; }
        public int PersonelID { get; set; }
        public Personel? Personel { get; set; }

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Onaylimi { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }
        public KayitDurumu? KayitDurumu { get; set; }
    }
}
