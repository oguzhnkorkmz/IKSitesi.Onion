using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IK.Application.Layer.Models.DTO_s.Login;
using IK.CoreLayer.Entities;
using IK.InfrastructureLayer.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ForgotPasswordController> _logger;

        public ForgotPasswordController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<ForgotPasswordController> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest_DTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email adresi boş olamaz.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Ok("Eğer bu e-posta adresine ait bir hesap varsa, şifre sıfırlama maili gönderilecektir.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            string resetPasswordUrl = $"{_configuration["ResetPasswordUrl"]}?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            var emailBody = $@"
                <p>Merhaba,</p>
                <p>Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklamanız yeterlidir:</p>
                <p><a href='{resetPasswordUrl}'>Şifre Sıfırlama Bağlantısı</a></p>
                <p>Eğer bu isteği siz yapmadıysanız, lütfen bu maili dikkate almayın.</p>";

            await _emailSender.SendEmailAsync(user.Email, "Şifre Sıfırlama Talebi", emailBody);

            return Ok("Eğer bu e-posta adresine ait bir hesap varsa, şifre sıfırlama maili gönderilecektir.");
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest_DTO request)
        {
            if (string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Token) ||
                string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Eksik bilgi var.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Ok("İşlem tamamlandı.");
            }

            try
            {
                // Eğer token tarayıcı/model binder tarafından decode edildiyse, burada decode etmeye gerek yok.
                // var decodedToken = System.Net.WebUtility.UrlDecode(request.Token);
                // Böylece doğrudan request.Token ile devam ediyoruz.
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Şifreniz başarıyla sıfırlandı.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

    }
}