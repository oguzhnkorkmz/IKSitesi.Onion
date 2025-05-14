using IK.Application.Layer.Models.DTO_s.Personel;
using IK.Application.Layer.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "KurumAdmin")]  // Sadece kurum yöneticileri personel ekleyebilir
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelService _personelService;

        public PersonelController(IPersonelService personelService)
        {
            _personelService = personelService;
        }

        /// <summary>
        /// Yeni personel ekler. Hem Personel tablosuna hem AspNetUsers tablosuna yazar.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonelEkle_DTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(PersonelResault_DTO.Failure("Geçersiz ya da eksik veri."));

           
            await _personelService.CreatePersonelAsync(dto);
            return Ok(PersonelResault_DTO.Success("Personel başarıyla oluşturuldu."));
          
        }
    }
}
