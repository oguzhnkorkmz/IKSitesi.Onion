using IK.Application.Layer.Models.DTO_s;
using IK.Application.Layer.Models.DTO_s.Bolum;
using IK.Application.Layer.Services.BolumService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolumController : ControllerBase
    {
        private readonly IBolumService _bolumService;

        public BolumController(IBolumService bolumService)
        {
            _bolumService = bolumService;
        }

        // ✅ Tüm bölümleri getir
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Bolum_DTO>>> GetBolumler()
        {
            var bolumler = await _bolumService.TumBolumlerAsync();
            if (bolumler == null || !bolumler.Any())
                return NotFound("Veritabanında bölüm bulunamadı.");

            return Ok(bolumler);
        }

        // ✅ ID ile Bölüm Getir
        [HttpGet("{id}")]
        public async Task<ActionResult<Bolum_DTO>> GetBolumById(int id)
        {
            var bolum = await _bolumService.BolumAraAsync(id);
            if (bolum == null)
                return NotFound("Bölüm bulunamadı.");

            return Ok(bolum);
        }

        // ✅ Yeni bölüm ekle
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBolum([FromBody] BolumEkle_DTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.BolumAdi))
                return BadRequest("Geçersiz bölüm bilgileri.");

            var bolumId = await _bolumService.BolumEkleAsync(dto);
            if (bolumId > 0)
                return Ok($"Bölüm başarıyla eklendi. Bölüm ID: {bolumId}");

            return BadRequest("Bölüm eklenirken hata oluştu.");
        }

        // ✅ Bölüm güncelleme
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBolum([FromBody] Bolum_DTO dto)
        {
            if (dto == null || dto.BolumID <= 0)
                return BadRequest("Geçersiz bölüm bilgileri.");

            await _bolumService.BolumGuncelleAsync(dto);
            return Ok("Bölüm başarıyla güncellendi.");
        }

        // ✅ Bölüm silme
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBolum(int id)
        {
            await _bolumService.BolumSilAsync(id);
            return Ok("Bölüm başarıyla silindi.");
        }
    }
}