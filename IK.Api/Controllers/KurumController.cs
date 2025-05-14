using System;
using System.Threading.Tasks;
using IK.Application.Layer.Services.KurumService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SiteAdmin")]
    public class KurumController : ControllerBase
    {
        private readonly IKurumService _kurumService;
        private readonly ILogger<KurumController> _logger;

        public KurumController(IKurumService kurumService, ILogger<KurumController> logger)
        {
            _kurumService = kurumService;
            _logger = logger;
        }

        // GET: api/Kurum/TumKurumlar
        [HttpGet("TumKurumlar")]
        public async Task<IActionResult> TumKurumlar()
        {
            try
            {
                var kurumDtos = await _kurumService.GetAllKurumsAsync();
                return Ok(kurumDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TumKurumlar endpoint'inde kurumlar getirilirken hata oluştu.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Kurum/KurumGetir/{id}
        [HttpGet("KurumGetir/{id}")]
        public async Task<IActionResult> KurumGetir(int id)
        {
            try
            {
                var kurumDto = await _kurumService.GetKurumByIdAsync(id);
                if (kurumDto == null)
                {
                    return NotFound("Kurum bulunamadı.");
                }
                return Ok(kurumDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"KurumGetir endpoint'inde (id: {id}) kurum verisi getirilirken hata oluştu.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}