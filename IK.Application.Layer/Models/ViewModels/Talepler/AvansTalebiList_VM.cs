using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.ViewModels.Talepler
{
    public class AvansTalebiList_VM
    {
        public int AvansTalebiID { get; set; } // Talep ID'si
        public decimal TalepEdilenTutar { get; set; }
        public int PersonelID { get; set; } // İlgili personelin ID'si
        public string PersonelAdi { get; set; } // Personelin adı ve soyadı

        public bool Onaylimi { get; set; } // Onay durumu
    }
}
