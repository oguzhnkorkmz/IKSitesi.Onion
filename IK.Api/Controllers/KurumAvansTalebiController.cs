using IK.Application.Layer.Models.ViewModels.Talepler;
using IK.Application.Layer.Services.AvansTalebi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KurumAdmin")]
    public class KurumAvansTalebiController : ControllerBase
    {
        private readonly IAvansTalebiService _avansTalebiService;
        private readonly ILogger<KurumAvansTalebiController> _logger;

        public KurumAvansTalebiController(IAvansTalebiService avansTalebiService, ILogger<KurumAvansTalebiController> logger)
        {
            _avansTalebiService = avansTalebiService;
            _logger = logger;
        }

        // GET: api/KurumAvansTalebi/bekleyen
        [HttpGet("tümtalepler")]
        public async Task<IActionResult> TumTalepler()
        {
            // Token'dan KurumID claim'ini alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
            {
                return Unauthorized("KurumID bilgisi alınamadı.");
            }

            // Servis katmanından, ilgili kurumID'ye ait tüm izin taleplerini alıyoruz.
            var talepler = await _avansTalebiService.GetAllTaleplerByKurumIDAsync(kurumId);
            var resultDto = talepler.Select(t => new AvansTalebiList_VM
            {
                AvansTalebiID= t.AvansTalebiID,
                PersonelID = t.PersonelID,
                // Personel adı oluşturma mantığınız, örneğin Personel entity içinde ad ve soyad bilgileri varsa:
                PersonelAdi = t.Personel != null ? t.Personel.PersonelAdi + " " + t.Personel.PersonelSoyadi : string.Empty,
                TalepEdilenTutar = t.TalepEdilenTutar,
                Onaylimi = t.Onaylimi
            }).ToList();

            return Ok(resultDto);
        }

        // PUT: api/KurumAvansTalebi/onayla/{avansTalebiID}
        [HttpPut("onayla/{avansTalebiID}")]
        public async Task<IActionResult> Onayla(int avansTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _avansTalebiService.OnaylaTalepAsync(avansTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "Avans talebi onaylandı." });
            else
                return BadRequest(new { Message = "Avans talebi onaylanamadı." });
        }

        // PUT: api/KurumAvansTalebi/reddet/{avansTalebiID}
        [HttpPut("reddet/{avansTalebiID}")]
        public async Task<IActionResult> Reddet(int avansTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _avansTalebiService.ReddetTalepAsync(avansTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "Avans talebi reddedildi." });
            else
                return BadRequest(new { Message = "Avans talebi reddedilemedi." });
        }
    }
}