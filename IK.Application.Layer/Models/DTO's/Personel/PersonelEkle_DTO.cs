using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Personel
{
    public class PersonelEkle_DTO
    {
        [Required(ErrorMessage = "Personel adı zorunludur.")]
        public string PersonelAdi { get; set; }

        [Required(ErrorMessage = "Personel soyadı zorunludur.")]
        public string PersonelSoyadi { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Bölüm seçimi zorunludur.")]
        public int BolumID { get; set; }


    }
}
