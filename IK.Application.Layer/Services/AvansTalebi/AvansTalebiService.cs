using IK.Application.Layer.Models.DTO_s.AvansTalebi;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.AvansTalebi
{
    public class AvansTalebiService : IAvansTalebiService
    {
        private readonly IAvansTalebiRepository _avansTalebiRepository;

        public AvansTalebiService(IAvansTalebiRepository avansTalebiRepository)
        {
            _avansTalebiRepository = avansTalebiRepository;
        }

        public async Task<int> AvansTalebiEkle(AvansTalebiEkle_DTO dto)
        {
            var avans = new CoreLayer.Entities.AvansTalebi
            {
                PersonelID = dto.PersonelID,
                TalepEdilenTutar = dto.TalepEdilenTutar,
                Onaylimi = false, // Oluşturma esnasında onay bekliyor
                EklenmeTarihi = DateTime.Now,
                KayitDurumu = KayitDurumu.KayitEkleme

            };

            return await _avansTalebiRepository.EkleAsync(avans);
        }

        public async Task<List<IK.CoreLayer.Entities.AvansTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId)
        {
            var result = await _avansTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.KurumID == kurumId && x.Onaylimi == false,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<List<IK.CoreLayer.Entities.AvansTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId)
        {
            // HerSekildeFiltreleAsync metodunu kullanarak;
            // - select: x => x (kimlik fonksiyonu) ile tüm entity'yi seçiyoruz,
            // - where: x => x.Personel.PersonelID == personelId && x.Onaylimi == true ile filtreleme yapıyoruz,
            // - orderBy: null,
            // - include: query => query.Include(x => x.Personel) ile Personel navigationı yüklüyoruz.
            var result = await _avansTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.PersonelID == personelId && x.Onaylimi == true,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<bool> OnaylaTalepAsync(int avansTalebiID, int kurumId)
        {
            // Öncelikle talebin geçerli ve kuruma ait olduğunu kontrol etmeliyiz.
            var talep = await _avansTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(avansTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Onay işlemi: Onaylimi'yi true yapıp, güncelleme tarihi atayabilirsiniz.
            talep.Onaylimi = true;
            talep.GuncellemeTarihi = DateTime.Now;
            // Repository'deki güncelleme metodu kullanılır.
            await _avansTalebiRepository.GuncelleAsync(talep);
            return true;
        }

        public async Task<bool> ReddetTalepAsync(int avansTalebiID, int kurumId)
        {
            // Talebin geçerliliğini benzer şekilde kontrol ediyoruz.
            var talep = await _avansTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(avansTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Red işlemi için farklı bir durum güncellemesi yapılabilir.
            // Örneğin, Onaylimi false kalır veya KayitDurumu farklı olarak işaretlenir.
            talep.KayitDurumu = KayitDurumu.KayitSil; // Örnek olarak red işareti
            talep.GuncellemeTarihi = DateTime.Now;

            await _avansTalebiRepository.GuncelleAsync(talep);
            return true;
        }
    }
}
