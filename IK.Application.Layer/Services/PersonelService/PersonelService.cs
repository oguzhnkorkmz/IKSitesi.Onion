using System.Net;
using IK.Application.Layer.Models.DTO_s.Personel;
using IK.Application.Layer.Services.PersonelService;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.EmailSender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

public class PersonelService : IPersonelService
{
    private readonly IPersonelRepository _personelRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;

    public PersonelService(
        IPersonelRepository personelRepository,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IEmailSender emailSender,
        IConfiguration configuration)
    {
        _personelRepository = personelRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task CreatePersonelAsync(PersonelEkle_DTO dto)
    {
        // 1. Personel tablosuna kayıt
        var kurumId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("KurumID")?.Value);

        var personel = new Personel
        {
            PersonelAdi = dto.PersonelAdi,
            PersonelSoyadi = dto.PersonelSoyadi,
            BolumID = dto.BolumID,
            KurumID = kurumId,
            EklenmeTarihi = DateTime.Now,
            KayitDurumu = IK.CoreLayer.Enums.KayitDurumu.KayitEkleme
        };

        // Personel tablosuna kaydediyoruz
        await _personelRepository.EkleAsync(personel);

        // 2. ApplicationUser oluştur (Personel ile bağlantılı)
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            PersonelID = personel.PersonelID, // PersonelID'yi ilişkilendiriyoruz
            KurumID = kurumId // KurumID'yi ilişkilendiriyoruz
        };

        // Kullanıcıyı oluşturuyoruz
        var result = await _userManager.CreateAsync(user, dto.Sifre);

        if (result.Succeeded)
        {
            // 3. Personel rolünü atıyoruz
            await _userManager.AddToRoleAsync(user, "Personel");

            // 4. Şifre sıfırlama bağlantısı oluştur
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            string resetPasswordUrl = $"{_configuration["ResetPasswordUrl"]}?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            // 5. Şifre belirleme bağlantısını içeren e-posta gönder
            var emailBody = $@"
                <p>Merhaba {dto.PersonelAdi},</p>
                <p>Hesabınızı tamamlamak için aşağıdaki bağlantıya tıklayarak şifrenizi belirleyebilirsiniz:</p>
                <p><a href='{resetPasswordUrl}'>Şifrenizi Belirleyin</a></p>
                <p>Eğer bu isteği siz yapmadıysanız, lütfen bu maili dikkate almayın.</p>";

            await _emailSender.SendEmailAsync(user.Email, "Şifre Belirleme", emailBody);
        }
        else
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}