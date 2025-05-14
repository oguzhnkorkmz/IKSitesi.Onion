using IK.Application.Layer.Models.ViewModels.Talepler;
using IK.Application.Layer.Services.HarcamaTalebi; // HarcamaTalebi servisinin namespace'ini kullanın.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KurumAdmin")]
    public class KurumHarcamaTalebiController : ControllerBase
    {
        private readonly IHarcamaTalebiService _harcamaTalebiService;
        private readonly ILogger<KurumHarcamaTalebiController> _logger;

        public KurumHarcamaTalebiController(IHarcamaTalebiService harcamaTalebiService, ILogger<KurumHarcamaTalebiController> logger)
        {
            _harcamaTalebiService = harcamaTalebiService;
            _logger = logger;
        }

        // GET: api/KurumHarcamaTalebi/bekleyen
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
            var talepler = await _harcamaTalebiService.GetAllTaleplerByKurumIDAsync(kurumId);
            var resultDto = talepler.Select(t => new HarcamaTalebiList_VM
            {
                HarcamaTalebiID = t.HarcamaTalebiID,
                PersonelID = t.PersonelID,
                // Personel adı oluşturma mantığınız, örneğin Personel entity içinde ad ve soyad bilgileri varsa:
                PersonelAdi = t.Personel != null ? t.Personel.PersonelAdi + " " + t.Personel.PersonelSoyadi : string.Empty,
                HarcamaTutari=t.HarcamaTutari,
                Aciklama=t.Aciklama,
                Onaylimi = t.Onaylimi
            }).ToList();

            return Ok(resultDto);
        }

        // PUT: api/KurumHarcamaTalebi/onayla/{harcamaTalebiID}
        [HttpPut("onayla/{harcamaTalebiID}")]
        public async Task<IActionResult> Onayla(int harcamaTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _harcamaTalebiService.OnaylaTalepAsync(harcamaTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "Harcama talebi onaylandı." });
            else
                return BadRequest(new { Message = "Harcama talebi onaylanamadı." });
        }

        // PUT: api/KurumHarcamaTalebi/reddet/{harcamaTalebiID}
        [HttpPut("reddet/{harcamaTalebiID}")]
        public async Task<IActionResult> Reddet(int harcamaTalebiID)
        {
            // Token'dan KurumID'yi alıyoruz.
            var kurumIdClaim = User.FindFirst("KurumID")?.Value;
            if (string.IsNullOrEmpty(kurumIdClaim) || !int.TryParse(kurumIdClaim, out var kurumId))
                return Unauthorized("KurumID bilgisi alınamadı.");

            bool result = await _harcamaTalebiService.ReddetTalepAsync(harcamaTalebiID, kurumId);
            if (result)
                return Ok(new { Message = "Harcama talebi reddedildi." });
            else
                return BadRequest(new { Message = "Harcama talebi reddedilemedi." });
        }
    }
}