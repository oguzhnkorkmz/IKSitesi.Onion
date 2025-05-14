using System.Collections.Generic;
using System.Threading.Tasks;
using IK.Application.Layer.Models.DTO_s.Kurum;

namespace IK.Application.Layer.Services.KurumService
{
    public interface IKurumService
    {
        /// <summary>
        /// Tüm kurumları DTO olarak getirir.
        /// </summary>
        Task<List<KurumList_DTO>> GetAllKurumsAsync();

        /// <summary>
        /// Belirtilen ID'ye ait kurumun detaylarını DTO olarak getirir.
        /// </summary>
        Task<KurumDetay_DTO> GetKurumByIdAsync(int id);
    }
}