using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Kurum
{
    public class KurumDetay_DTO
    {
        public int KurumID { get; set; }
        public string KurumAdi { get; set; }
        public string Adres { get; set; }
        public int PaketID { get; set; }

        // Eğer Paket'e ait detaylı bilgileri göstermek isterseniz, ayrı bir DTO (örneğin PaketDto) oluşturabilirsiniz.
        // Burada sadece örnek olması açısından Paket objesinin kısa bilgilerini ekledik.
        public string PaketBilgi { get; set; }  // Örneğin, Paket adını ya da özet bilgisini tutabilirsiniz.

        public DateTime PaketBaslangicTarihi { get; set; }
        public DateTime PaketBitisTarihi { get; set; }
        public bool PaketAktifMi { get; set; }
        public string VergiNumarasi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? SilmeTarihi { get; set; }

        // Enum değerlerini string olarak dönüştürmek, UI tarafında okunabilirlik açısından avantajlı olabilir.
        public string KayitDurumu { get; set; }

    }
}
