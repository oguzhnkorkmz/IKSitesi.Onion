using IK.Application.Layer.Models.DTO_s.AvansTalebi;
using IK.Application.Layer.Models.ViewModels.Talepler;
using IK.Application.Layer.Services.AvansTalebi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvansTalebiController : ControllerBase
    {
        private readonly IAvansTalebiService _avansTalebiService;

        public AvansTalebiController(IAvansTalebiService avansTalebiService)
        {
            _avansTalebiService = avansTalebiService;
        }

        [HttpPost("ekle")]
        public async Task<ActionResult> Ekle([FromBody] AvansTalebiEkle_DTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int id = await _avansTalebiService.AvansTalebiEkle(dto);
            return Ok(new { AvansTalebiID = id, Message = "Avans talebi başarıyla oluşturuldu." });
        }

        [HttpGet("onaylanmis")]
        public async Task<IActionResult> GetApprovedTalep()
        {
            // Kullanıcının token’ından PersonelID claim’ini alıyoruz.
            var personelIdClaim = User.FindFirst("PersonelID")?.Value;
            if (string.IsNullOrEmpty(personelIdClaim) || !int.TryParse(personelIdClaim, out int personelId))
                return Unauthorized("Personel ID bilgisi alınamadı.");

            // Onaylanmış (Onaylimi == true) izin taleplerini servisten alıyoruz.
            var approvedTalepler = await _avansTalebiService.GetApprovedTaleplerByPersonelIdAsync(personelId);

            // Entity'leri DTO'ya map ederek istemciye gönderiyoruz.
            var resultDto = approvedTalepler.Select(t => new AvansTalebiList_VM
            {
                AvansTalebiID = t.AvansTalebiID,
                PersonelID = t.PersonelID,
                // Personel adı oluşturma mantığınız, örneğin Personel entity içinde ad ve soyad bilgileri varsa:
                PersonelAdi = t.Personel != null ? t.Personel.PersonelAdi + " " + t.Personel.PersonelSoyadi : string.Empty,
                TalepEdilenTutar = t.TalepEdilenTutar,
                Onaylimi = t.Onaylimi
            }).ToList();

            return Ok(resultDto);
        }
    }
}
