using IK.Application.Layer.Models.DTO_s;
using IK.Application.Layer.Models.DTO_s.Paket;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.PaketService
{
    public class PaketService : IPaketService
    {
        private readonly IPaketRepository _paketRepository;

        public PaketService(IPaketRepository paketRepository)
        {
            _paketRepository = paketRepository;

        }
        public async Task<List<PaketList_DTO>> TumPaketlerAsync()
        {
            var paketler = await _paketRepository.ListeleAsync();

            return paketler.Select(p => new PaketList_DTO
            {
                PaketID = p.PaketID,
                PaketAdi = p.PaketAdi
            }).ToList();
        }

        public async Task<int> EklePaketAsync(PaketCreate_DTO paketDTO)
        {
            var paket = new Paket
            {
                PaketAdi = paketDTO.PaketAdi,
                Fiyat = paketDTO.Fiyat,
                PaketSuresi = paketDTO.PaketSuresi,
                AktifMi = paketDTO.AktifMi,
                KapasiteSayisi = paketDTO.KapasiteSayisi
                // EklenmeTarihi, KayitDurumu gibi alanlar repository üzerinde ayarlanıyor.
            };

            int id = await _paketRepository.EkleAsync(paket);
            return id;
        }

        // Var olan bir paketi güncelleme (PaketUpdate_DTO → Paket)
        public async Task GuncellePaketAsync(PaketUpdate_DTO paketDTO)
        {
            // Güncelleme öncesi mevcut paket bilgisini getiriyoruz
            var paket = await _paketRepository.AraAsync(paketDTO.PaketID);
            if (paket != null)
            {
                paket.PaketAdi = paketDTO.PaketAdi;
                paket.Fiyat = paketDTO.Fiyat;
                paket.PaketSuresi = paketDTO.PaketSuresi;
                paket.AktifMi = paketDTO.AktifMi;
                paket.KapasiteSayisi = paketDTO.KapasiteSayisi;

                await _paketRepository.GuncelleAsync(paket);
            }
            // Eğer bulunamazsa, hata fırlatabilir ya da sessizce devam edebilirsiniz.
        }

        // Paketi logical deletion yöntemiyle silme işlemi
        public async Task SilPaketAsync(int paketId)
        {
            await _paketRepository.SilAsync(paketId);
        }

        // Belirli bir paketi ID üzerinden getirme (PaketDetail_DTO dönüşümü)
        public async Task<PaketDetail_DTO> GetPaketByIdAsync(int paketId)
        {
            var paket = await _paketRepository.AraAsync(paketId);
            if (paket == null)
                return null;

            var dto = new PaketDetail_DTO
            {
                PaketID = paket.PaketID,
                PaketAdi = paket.PaketAdi,
                Fiyat = paket.Fiyat,
                PaketSuresi = paket.PaketSuresi,
                AktifMi = paket.AktifMi,
                KapasiteSayisi = paket.KapasiteSayisi,
                EklenmeTarihi = paket.EklenmeTarihi,
                GuncellemeTarihi = paket.GuncellemeTarihi,
                SilmeTarihi = paket.SilmeTarihi,
                KayitDurumu = paket.KayitDurumu?.ToString()
            };

            return dto;
        }
    }

}

