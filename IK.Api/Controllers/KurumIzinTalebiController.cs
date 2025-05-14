using IK.Application.Layer.Models.ViewModels.Talepler;
using IK.Application.Layer.Services.IzinTalebi;
using IK.Application.Layer.Services.IzinTalebiService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KurumAdmin")]
    public class KurumIzinTalebiController : ControllerBase
    {
        private readonly IIzinTalebiService _izinTalebiService;
        private readonly ILogger<KurumIzinTalebiController> _logger;

        public KurumIzinTalebiController(IIzinTalebiService izinTalebiService, ILogger<KurumIzinTalebiController> logger)
        {
            _izinTalebiService = izinTalebiService;
            _logger = logger;
        }

        // GET: api/KurumIzinTalebi/bekleyen
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
            var talepler = await _izinTalebiService.GetAllTaleplerByKurumIDAsync(kurumId);
            var resultDto = talepler.Select(t => new IzinTalebiList_VM
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

        // PUT: api/KurumIzinTalebi/onayla/{izinTalebiID}
        [HttpPut("onayla/{izinTalebiID}")]
        public async Task<IActionResult> Onayla(int izinTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _izinTalebiService.OnaylaTalepAsync(izinTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "İzin talebi onaylandı." });
            else
                return BadRequest(new { Message = "İzin talebi onaylanamadı." });
        }

        // PUT: api/KurumIzinTalebi/reddet/{izinTalebiID}
        [HttpPut("reddet/{izinTalebiID}")]
        public async Task<IActionResult> Reddet(int izinTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _izinTalebiService.ReddetTalepAsync(izinTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "İzin talebi reddedildi." });
            else
                return BadRequest(new { Message = "İzin talebi reddedilemedi." });
        }
    }
}