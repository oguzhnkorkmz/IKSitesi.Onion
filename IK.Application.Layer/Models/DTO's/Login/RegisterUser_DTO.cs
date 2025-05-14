using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Models.DTO_s.Login
{
    public class RegisterUser_DTO
    {

            [Required(ErrorMessage = "Kurum adı gereklidir.")]
            public string KurumAdi { get; set; }

            [Required(ErrorMessage = "Adres gereklidir.")]
            public string Adres { get; set; }

            [Required(ErrorMessage = "Vergi numarası gereklidir.")]
            public string VergiNumarasi { get; set; }

            [Required(ErrorMessage = "Paket seçimi gereklidir.")]
            public int PaketID { get; set; }    // ← Burada

            [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Email adresi gereklidir.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifre gereklidir.")]
            [MinLength(6)]
            public string Sifre { get; set; }

            [Required(ErrorMessage = "Şifre onayı gereklidir.")]
            [Compare("Sifre")]
            public string SifreOnayi { get; set; }
        


    }
}

