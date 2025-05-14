using IK.Application.Layer.Helpers;
using IK.Application.Layer.Models.DTO_s.Login;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using IK.InfrastructureLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IKDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginService(
            IKDBContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<LoginResult_DTO> LoginAsync(Login_DTO login)
        {
            // 1) Kullanıcıyı email ile bul
            var user = await _userManager.Users
                         .Include(u => u.Kurum)
                         .FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
                return null;

            // 2) Şifre doğrulaması
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Sifre, false);
            if (!signInResult.Succeeded)
                return null;

            // 3) Rolleri al (varsayılan olarak "Personel" atıyoruz)
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Personel";

            // 4) Claim listesi oluştur
            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            // PersonelID claim'i ekle (varsa)
            if (user.PersonelID.HasValue)
                claims.Add(new Claim("PersonelID", user.PersonelID.Value.ToString()));

            // KurumID claim'i ekle (varsa)
            if (user.KurumID.HasValue)
                claims.Add(new Claim("KurumID", user.KurumID.Value.ToString()));

            // 5) TokenHelper'ı kullanarak token üretiyoruz.
            // Kullanıcı PersonelID'ye sahipse o değeri, yoksa 0 değerini gönderiyoruz.
            var token = TokenHelper.GenerateToken(claims, _configuration, user.PersonelID.HasValue ? user.PersonelID.Value : 0, out DateTime expiration);

            // 6) Login sonucunu döndür
            return new LoginResult_DTO
            {
                UserId = user.Id,
                KurumID = user.KurumID,
                Role = role,
                Token = token,
                TokenExpiration = expiration
            };
        }

        public async Task<string> RegisterUserAsync(RegisterUser_DTO user)
        {
            // 1) Seçilen paketi kontrol et
            var paket = await _dbContext.Paketler
                .FirstOrDefaultAsync(p => p.PaketID == user.PaketID
                                       && p.KayitDurumu != KayitDurumu.KayitSil);
            if (paket == null)
                return "Seçilen paket geçersiz veya aktif değil.";

            // 2) Yeni Kurum oluştur ve kaydet
            var now = DateTime.UtcNow;
            var kurum = new Kurum
            {
                KurumAdi = user.KurumAdi,
                Adres = user.Adres,
                VergiNumarasi = user.VergiNumarasi,
                PaketID = paket.PaketID,
                PaketBaslangicTarihi = now,
                PaketBitisTarihi = now.AddMonths(1),
                PaketAktifMi = true,
                EklenmeTarihi = now,
                KayitDurumu = KayitDurumu.KayitEkleme
            };
            await _dbContext.Kurumlar.AddAsync(kurum);
            await _dbContext.SaveChangesAsync();

            // 3) Kuruma bağlı ApplicationUser oluştur
            var newUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                KurumID = kurum.KurumID,
            };
            var createResult = await _userManager.CreateAsync(newUser, user.Sifre);
            if (!createResult.Succeeded)
            {
                _dbContext.Kurumlar.Remove(kurum);
                await _dbContext.SaveChangesAsync();
                return string.Join(" | ", createResult.Errors.Select(e => e.Description));
            }

            // 4) Role ataması: KurumAdmin
            var roleResult = await _userManager.AddToRoleAsync(newUser, "KurumAdmin");
            if (!roleResult.Succeeded)
                return string.Join(" | ", roleResult.Errors.Select(e => e.Description));

            return "Başarılı";
        }

        public Task<int> GetUserIDAsync(ClaimsPrincipal claim)
        {
            var userIdClaim = claim.FindFirst(ClaimTypes.NameIdentifier);
            return Task.FromResult(userIdClaim != null
                ? int.Parse(userIdClaim.Value)
                : -1);
        }
    }
}