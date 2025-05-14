using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken(List<Claim> claims, IConfiguration configuration, int personelId, out DateTime expiration)
        {
            // Eğer PersonelID claim'i eklenmemişse ekleyelim
            if (!claims.Any(c => c.Type == "PersonelID"))
            {
                claims.Add(new Claim("PersonelID", personelId.ToString()));
            }

            // Kullanıcı rolünü dinamik ekleyin (eğer mevcut değilse)
            var userRole = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userRole))
            {
                claims.Add(new Claim(ClaimTypes.Role, "SiteAdmin")); // Varsayılan rol
            }

            // Secret key ve imzalama bilgileri için konfigürasyondan verileri çekiyoruz.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token geçerlilik süresini belirliyoruz.
            expiration = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpireMinutes"]));

            // JwtSecurityToken nesnesini oluşturuyoruz.
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Token'ı string olarak dışarı veriyoruz.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}