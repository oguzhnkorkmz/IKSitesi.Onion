using IK.Application.Layer.Models.DTO_s;
using IK.Application.Layer.Models.DTO_s.Bolum;
using IK.CoreLayer.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.BolumService
{
    public interface IBolumService
    {
        Task<List<Bolum_DTO>> TumBolumlerAsync(); // Tüm bölümleri getir
        Task<int> BolumEkleAsync(BolumEkle_DTO dto); // Yeni bölüm ekle
        Task BolumGuncelleAsync(Bolum_DTO dto); // Bölümü güncelle
        Task BolumSilAsync(int id); // Bölümü sil
        Task<Bolum_DTO> BolumAraAsync(int BolumID);
    }
}
