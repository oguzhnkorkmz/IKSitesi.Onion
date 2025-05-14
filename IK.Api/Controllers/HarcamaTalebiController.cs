using IK.Application.Layer.Models.DTO_s.HarcamaTalebi;
using IK.Application.Layer.Models.ViewModels.Talepler;
using IK.Application.Layer.Services.HarcamaTalebi;
using IK.Application.Layer.Services.HarcamaTalebiService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HarcamaTalebiController : ControllerBase
    {
        private readonly IHarcamaTalebiService _harcamaTalebiService;

        public HarcamaTalebiController(IHarcamaTalebiService harcamaTalebiService)
        {
            _harcamaTalebiService = harcamaTalebiService;
        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] HarcamaTalebiEkle_DTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
                int harcamaTalebiId = await _harcamaTalebiService.EkleHarcamaTalebiAsync(dto);
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
            var approvedTalepler = await _harcamaTalebiService.GetApprovedTaleplerByPersonelIdAsync(personelId);

            // Entity'leri DTO'ya map ederek istemciye gönderiyoruz.
            var resultDto = approvedTalepler.Select(t => new HarcamaTalebiList_VM
            {
                HarcamaTalebiID = t.HarcamaTalebiID,
                PersonelID = t.PersonelID,
                // Personel adı oluşturma mantığınız, örneğin Personel entity içinde ad ve soyad bilgileri varsa:
                PersonelAdi = t.Personel != null ? t.Personel.PersonelAdi + " " + t.Personel.PersonelSoyadi : string.Empty,
                HarcamaTutari = t.HarcamaTutari,
                Onaylimi = t.Onaylimi
            }).ToList();

            return Ok(resultDto);
        }
    }
}