using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Paket
{
    public class PaketCreate_DTO
    {
        public string PaketAdi { get; set; }
        public decimal Fiyat { get; set; }
        public int PaketSuresi { get; set; }
        public bool AktifMi { get; set; }
        public int KapasiteSayisi { get; set; }
    }

}
