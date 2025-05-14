using System.ComponentModel.DataAnnotations;

namespace IK.Application.Layer.Models.DTO_s.Login
{
    public class ResetPasswordRequest_DTO
    {
        [Required(ErrorMessage = "E-posta gerekli.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token gerekli.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Yeni şifre gerekli.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı gerekli.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}