using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IK.Application.Layer.Models.DTO_s;
using IK.Application.Layer.Models.DTO_s.Paket;
using IK.Application.Layer.Services.PaketService;
using IK.CoreLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaketController : ControllerBase
    {
        private readonly IPaketService _paketService;

        public PaketController(IPaketService paketService)
        {
            _paketService = paketService;
        }

        // GET: api/Paket
        // Bu aksiyon paket listelemeyi sağlar (değiştirilmeden kalacaktır)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaketList_DTO>>> GetPaketler()
        {
            var paketler = await _paketService.TumPaketlerAsync();
            if (paketler == null || !paketler.Any())
            {
                return NotFound("Veritabanında paket bulunamadı.");
            }
            return Ok(paketler);
        }

        // GET: api/Paket/GetPaketById/5
        [HttpGet("GetPaketById/{id}")]
        public async Task<ActionResult<PaketDetail_DTO>> GetPaketById(int id)
        {
            var paket = await _paketService.GetPaketByIdAsync(id);
            if (paket == null)
            {
                return NotFound("Paket bulunamadı.");
            }
            return Ok(paket);
        }

        [Authorize(Roles = "SiteAdmin")]
        [HttpPost("Ekle")]
        public async Task<ActionResult<int>> EklePaket([FromBody] PaketCreate_DTO paketDTO)
        {
            if (paketDTO == null)
            {
                return BadRequest("Geçerli bir paket verisi sağlanmadı.");
            }
            int id = await _paketService.EklePaketAsync(paketDTO);
            // İsteğe bağlı; CreatedAtAction ile location header gönderebilirsiniz.
            return Ok(id);
        }

        [Authorize(Roles = "SiteAdmin")]
        [HttpPut("Guncelle")]
        public async Task<IActionResult> GuncellePaket([FromBody] PaketUpdate_DTO paketDTO)
        {
            if (paketDTO == null)
            {
                return BadRequest("Geçerli bir paket verisi sağlanmadı.");
            }
            await _paketService.GuncellePaketAsync(paketDTO);
            return Ok("Paket güncellendi.");
        }

        [Authorize(Roles = "SiteAdmin")]
        [HttpDelete("Sil/{id}")]
        public async Task<IActionResult> SilPaket(int id)
        {
            await _paketService.SilPaketAsync(id);
            return Ok("Paket silindi.");
        }
    }
}