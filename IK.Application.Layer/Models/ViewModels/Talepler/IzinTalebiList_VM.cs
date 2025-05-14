using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.ViewModels.Talepler
{
    public class IzinTalebiList_VM
    {
        public int IzinTalebiID { get; set; } // Talep ID'si
        public int PersonelID { get; set; } // İlgili personelin ID'si
        public string PersonelAdi { get; set; } // Personelin adı ve soyadı
        public DateTime BaslangicTarihi { get; set; } // İzin başlangıç tarihi
        public DateTime BitisTarihi { get; set; } // İzin bitiş tarihi
        public bool Onaylimi { get; set; } // Onay durumu
    }
}
