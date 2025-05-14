using IK.Application.Layer.Models.DTO_s.Login;
using IK.Application.Layer.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // Şirket (Kurum/Site Yöneticisi) girişi
        [HttpPost("company-login")]
        public async Task<IActionResult> CompanyLogin([FromBody] Login_DTO dto)
        {
            var result = await _loginService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Geçersiz kullanıcı veya şifre.");

            // Sadece kurum veya site yöneticilerinin girişi kabul ediliyor
            var allowed = new[] { "KurumAdmin", "SiteAdmin" };
            if (!allowed.Contains(result.Role))
                return Forbid("Bu giriş yalnızca kurum veya site yöneticilerine açık.");

            return Ok(result);
        }

        // Personel girişi
        [HttpPost("personnel-login")]
        public async Task<IActionResult> PersonnelLogin([FromBody] Login_DTO dto)
        {
            var result = await _loginService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Geçersiz kullanıcı veya şifre.");

            if (result.Role != "Personel")
                return Forbid("Bu giriş yalnızca personel kullanıcılarına açık.");

            return Ok(result);
        }

        // Yeni bir kurum admini kaydı (Register)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser_DTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loginService.RegisterUserAsync(registerDto);
            if (result != "Başarılı")
                return BadRequest(result);

            return Ok("Kayıt başarılı.");
        }

        // Oturum açmış kullanıcının ID'sini döndürür
        [HttpGet("me/id")]
        public async Task<IActionResult> GetMyUserId()
        {
            var userId = await _loginService.GetUserIDAsync(User);
            if (userId < 0)
                return Unauthorized();

            return Ok(userId);
        }
    }
}