using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.AvansTalebi
{
    public class AvansTalebiEkle_DTO
    {
        // Giriş yapan personelin token/Identity'den alınan PersonelID'si API'ye bu şekilde gönderilecek.
        public int PersonelID { get; set; }
        public decimal TalepEdilenTutar { get; set; }

    }
}
