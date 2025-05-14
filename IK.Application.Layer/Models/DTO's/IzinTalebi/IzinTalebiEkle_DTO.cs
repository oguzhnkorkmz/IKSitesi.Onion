using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.IzinTalebi
{
    public class IzinTalebiEkle_DTO
    {
        public int PersonelID { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
