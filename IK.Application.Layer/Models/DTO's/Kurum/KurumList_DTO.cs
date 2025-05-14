using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Kurum
{
    public class KurumList_DTO
    {
        public int KurumID { get; set; }
        public string KurumAdi { get; set; }
        public DateTime PaketBaslangicTarihi { get; set; }
        public DateTime PaketBitisTarihi { get; set; }
        public bool PaketAktifMi { get; set; }

    }
}
