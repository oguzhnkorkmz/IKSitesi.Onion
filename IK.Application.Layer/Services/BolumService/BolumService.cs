using System.Collections.Generic;
using System.Threading.Tasks;
using IK.Application.Layer.Models.DTO_s;
using IK.Application.Layer.Models.DTO_s.Bolum;
using IK.Application.Layer.Models.DTO_s.Paket;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Concretes;

namespace IK.Application.Layer.Services.BolumService
{
    public class BolumService : IBolumService
    {
        private readonly IBolumRepository _bolumRepository;

        public BolumService(IBolumRepository bolumRepository)
        {
            _bolumRepository = bolumRepository;
        }

        // ✅ Tüm Bölümleri Listele
        public async Task<List<Bolum_DTO>> TumBolumlerAsync()
        {
            var bolumler = await _bolumRepository.ListeleAsync();
            return bolumler.Select(b => new Bolum_DTO
            {
                BolumID = b.BolumID,
                BolumAdi = b.BolumAdi
            }).ToList();
        }

        // ✅ ID ile Bölüm Getir
    

        // ✅ Yeni Bölüm Ekle
        public async Task<int> BolumEkleAsync(BolumEkle_DTO dto)
        {
            var yeniBolum = new Bolum
            {
                BolumAdi = dto.BolumAdi,
            };

            await _bolumRepository.EkleAsync(yeniBolum);
            return yeniBolum.BolumID;   
        }

        // ✅ Bölüm Güncelle
        public async Task BolumGuncelleAsync(Bolum_DTO dto)
        {
            var bolum = await _bolumRepository.AraAsync(dto.BolumID);
            if (bolum != null) 
                bolum.BolumAdi = dto.BolumAdi;
           

            await _bolumRepository.GuncelleAsync(bolum);
        }

        // ✅ Bölüm Sil
        public async Task BolumSilAsync(int id)
        {
            await _bolumRepository.SilAsync(id);
        }

        public async Task<Bolum_DTO> BolumAraAsync(int bolumID)
        {
            var bolum = await _bolumRepository.AraAsync(bolumID);
            if (bolum == null)
                return null;

            return new Bolum_DTO
            {
                BolumID = bolum.BolumID,
                BolumAdi = bolum.BolumAdi
            };
        }
    }
}