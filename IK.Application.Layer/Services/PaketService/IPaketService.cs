using IK.Application.Layer.Models.DTO_s.Paket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.PaketService
{
    public interface IPaketService
    {
        Task<List<PaketList_DTO>> TumPaketlerAsync();
        Task<int> EklePaketAsync(PaketCreate_DTO paketDTO);

        // Varolan paketi güncelleme
        Task GuncellePaketAsync(PaketUpdate_DTO paketDTO);

        // Paketi silme (logical deletion)
        Task SilPaketAsync(int paketId);

        // Belirli bir paketi ID üzerinden getirme
        Task<PaketDetail_DTO> GetPaketByIdAsync(int paketId);

    }
}
