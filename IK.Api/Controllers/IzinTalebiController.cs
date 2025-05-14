using IK.Application.Layer.Models.DTO_s.IzinTalebi;
using IK.Application.Layer.Services.IzinTalebiService; // veya IIzinTalebiService namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using IK.Application.Layer.Services.IzinTalebi;
using IK.Application.Layer.Models.ViewModels.Talepler;

namespace IK.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IzinTalebiController : ControllerBase
    {
        private readonly IIzinTalebiService _izinTalebiService;

        public IzinTalebiController(IIzinTalebiService izinTalebiService)
        {
            _izinTalebiService = izinTalebiService;

        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] IzinTalebiEkle_DTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            int harcamaTalebiId = await _izinTalebiService.IzinTalebiEkleAsync(dto);
            return Ok(new { HarcamaTalebiID = harcamaTalebiId, Message = "Harcama talebi başarıyla oluşturuldu." });

        }

        [HttpGet("onaylanmis")]
        public async Task<IActionResult> GetApprovedTalep()
        {
            // Kullanıcının token’ından PersonelID claim’ini alıyoruz.
            var personelIdClaim = User.FindFirst("PersonelID")?.Value;
            if (string.IsNullOrEmpty(personelIdClaim) || !int.TryParse(personelIdClaim, out int personelId))
                return Unauthorized("Personel ID bilgisi alınamadı.");

            // Onaylanmış (Onaylimi == true) izin taleplerini servisten alıyoruz.
            var approvedTalepler = await _izinTalebiService.GetApprovedTaleplerByPersonelIdAsync(personelId);

            // Entity'leri DTO'ya map ederek istemciye gönderiyoruz.
            var resultDto = approvedTalepler.Select(t => new IzinTalebiList_VM
            {
                IzinTalebiID = t.IzinTalebiID,
                PersonelID = t.PersonelID,
                // Personel adı oluşturma mantığınız, örneğin Personel entity içinde ad ve soyad bilgileri varsa:
                PersonelAdi = t.Personel != null ? t.Personel.PersonelAdi + " " + t.Personel.PersonelSoyadi : string.Empty,
                BaslangicTarihi = t.BaslangicTarihi,
                BitisTarihi = t.BitisTarihi,
                Onaylimi = t.Onaylimi
            }).ToList();

            return Ok(resultDto);
        }

    }
}