using IK.Application.Layer.Models.DTO_s.HarcamaTalebi;
using IK.Application.Layer.Services.HarcamaTalebi;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Enums;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.HarcamaTalebiService
{
    public class HarcamaTalebiService : IHarcamaTalebiService
    {
        private readonly IHarcamaTalebiRepository _harcamaTalebiRepository;

        public HarcamaTalebiService(IHarcamaTalebiRepository harcamaTalebiRepository)
        {
            _harcamaTalebiRepository = harcamaTalebiRepository;
        }

        public async Task<int> EkleHarcamaTalebiAsync(HarcamaTalebiEkle_DTO dto)
        {
            var entity = new CoreLayer.Entities.HarcamaTalebi
            {
                PersonelID = dto.PersonelID,
                HarcamaTutari = dto.HarcamaTutari,
                Aciklama = dto.Aciklama,
                Onaylimi = false, // Oluşturma esnasında default olarak onaysız
                EklenmeTarihi = DateTime.Now,
                KayitDurumu = KayitDurumu.KayitEkleme
            };

            // Repository'nizde override edilmiş EkleAsync metodunu veya BaseRepository'deki metodu çağırın.
            return await _harcamaTalebiRepository.EkleAsync(entity);
        }

        public async Task<List<IK.CoreLayer.Entities.HarcamaTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId)
        {
            var result = await _harcamaTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.KurumID == kurumId && x.Onaylimi == false,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<List<IK.CoreLayer.Entities.HarcamaTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId)
        {
            // HerSekildeFiltreleAsync metodunu kullanarak;
            // - select: x => x (kimlik fonksiyonu) ile tüm entity'yi seçiyoruz,
            // - where: x => x.Personel.PersonelID == personelId && x.Onaylimi == true ile filtreleme yapıyoruz,
            // - orderBy: null,
            // - include: query => query.Include(x => x.Personel) ile Personel navigationı yüklüyoruz.
            var result = await _harcamaTalebiRepository.HerSekildeFiltreleAsync(
                 x => x,
                 x => x.Personel.PersonelID == personelId && x.Onaylimi == true,
                 null,
                 query => query.Include(x => x.Personel)
            );

            return result.ToList();
        }
        public async Task<bool> OnaylaTalepAsync(int harcamaTalebiID, int kurumId)
        {
            // Öncelikle talebin geçerli ve kuruma ait olduğunu kontrol etmeliyiz.
            var talep = await _harcamaTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(harcamaTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Onay işlemi: Onaylimi'yi true yapıp, güncelleme tarihi atayabilirsiniz.
            talep.Onaylimi = true;
            talep.GuncellemeTarihi = DateTime.Now;
            // Repository'deki güncelleme metodu kullanılır.
            await _harcamaTalebiRepository.GuncelleAsync(talep);
            return true;
        }

        public async Task<bool> ReddetTalepAsync(int harcamaTalebiID, int kurumId)
        {
            // Talebin geçerliliğini benzer şekilde kontrol ediyoruz.
            var talep = await _harcamaTalebiRepository.GetIzinTalebiForAvansAndHarcamaAsync(harcamaTalebiID);
            if (talep == null || talep.Personel == null || talep.Personel.KurumID != kurumId)
            {
                return false;
            }

            // Red işlemi için farklı bir durum güncellemesi yapılabilir.
            // Örneğin, Onaylimi false kalır veya KayitDurumu farklı olarak işaretlenir.
            talep.KayitDurumu = KayitDurumu.KayitSil; // Örnek olarak red işareti
            talep.GuncellemeTarihi = DateTime.Now;

            await _harcamaTalebiRepository.GuncelleAsync(talep);
            return true;
        }
    }
}