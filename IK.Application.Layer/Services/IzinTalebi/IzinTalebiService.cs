using IK.Application.Layer.Models.DTO_s.IzinTalebi;
using IK.Application.Layer.Services.IzinTalebi;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums; // KayitDurumu enum'unuzun tanımlı olduğu namespace
using IK.CoreLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.IzinTalebiService
{
    public class IzinTalebiService : IIzinTalebiService
    {
        private readonly IIzinTalebiRepository _izinTalebiRepository;
        private readonly ILogger<IzinTalebiService> _logger;

        public IzinTalebiService(IIzinTalebiRepository izinTalebiRepository, ILogger<IzinTalebiService> logger)
        {
            _izinTalebiRepository = izinTalebiRepository;
            _logger = logger;
        }

        public async Task<int> IzinTalebiEkleAsync(IzinTalebiEkle_DTO dto)
        {
           
                var izinTalebi = new CoreLayer.Entities.IzinTalebi
                {
                    PersonelID = dto.PersonelID,
                    BaslangicTarihi = dto.BaslangicTarihi,
                    BitisTarihi = dto.BitisTarihi,
                    Onaylimi = false,

                    // Zorunlu alanlar için atamalar:
                    EklenmeTarihi = DateTime.UtcNow,
                    // Varsayıya göre kayıt ekleme aşamasında KayitDurumu set ediliyor; sizin enum değeriniz farklı olabilir!
                    KayitDurumu = KayitDurumu.KayitEkleme
                };

               return await _izinTalebiRepository.EkleAsync(izinTalebi);
             
        }
        public async Task<List<IK.CoreLayer.Entities.IzinTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId)
        {
            var result = await _izinTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.KurumID == kurumId && x.Onaylimi == false,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<List<IK.CoreLayer.Entities.IzinTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId)
        {
            // HerSekildeFiltreleAsync metodunu kullanarak;
            // - select: x => x (kimlik fonksiyonu) ile tüm entity'yi seçiyoruz,
            // - where: x => x.Personel.PersonelID == personelId && x.Onaylimi == true ile filtreleme yapıyoruz,
            // - orderBy: null,
            // - include: query => query.Include(x => x.Personel) ile Personel navigationı yüklüyoruz.
            var result = await _izinTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.PersonelID == personelId && x.Onaylimi == true,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<bool> OnaylaTalepAsync(int izinTalebiID, int kurumId)
        {
            // Öncelikle talebin geçerli ve kuruma ait olduğunu kontrol etmeliyiz.
            var talep = await _izinTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(izinTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Onay işlemi: Onaylimi'yi true yapıp, güncelleme tarihi atayabilirsiniz.
            talep.Onaylimi = true;
            talep.GuncellemeTarihi = DateTime.Now;
            // Repository'deki güncelleme metodu kullanılır.
            await _izinTalebiRepository.GuncelleAsync(talep);
            return true;
        }

        public async Task<bool> ReddetTalepAsync(int izinTalebiID, int kurumId)
        {
            // Talebin geçerliliğini benzer şekilde kontrol ediyoruz.
            var talep = await _izinTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(izinTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Red işlemi için farklı bir durum güncellemesi yapılabilir.
            // Örneğin, Onaylimi false kalır veya KayitDurumu farklı olarak işaretlenir.
            talep.KayitDurumu = KayitDurumu.KayitSil; // Örnek olarak red işareti
            talep.GuncellemeTarihi = DateTime.Now;

            await _izinTalebiRepository.GuncelleAsync(talep);
            return true;
        }


    }
}