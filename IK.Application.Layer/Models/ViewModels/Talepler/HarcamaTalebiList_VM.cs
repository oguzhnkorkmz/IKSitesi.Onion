using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.ViewModels.Talepler
{
    public class HarcamaTalebiList_VM
    {
        public int HarcamaTalebiID { get; set; } // Talep ID'si
        public int PersonelID { get; set; } // İlgili personelin ID'si
        public string PersonelAdi { get; set; } // Personelin adı ve soyadı
        public string Aciklama { get; set; } // Personelin adı ve soyadı

        public decimal HarcamaTutari { get; set; }

        public bool Onaylimi { get; set; } // Onay durumu
    }
}
