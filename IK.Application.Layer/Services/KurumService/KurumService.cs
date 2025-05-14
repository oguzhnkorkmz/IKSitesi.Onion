using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Include için gerekli
using Microsoft.EntityFrameworkCore.Query; // IIncludableQueryable için gerekli olabilir
using IK.Application.Layer.Models.DTO_s.Kurum;
using IK.Application.Layer.Services.KurumService;
using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;

namespace IK.ServiceLayer
{
    public class KurumService : IKurumService
    {
        private readonly IKurumRepository _kurumRepository;

        public KurumService(IKurumRepository kurumRepository)
        {
            _kurumRepository = kurumRepository;
        }

        public async Task<List<KurumList_DTO>> GetAllKurumsAsync()
        {
            // Paket bilgilerini de dahil etmek için hersekilde filtrele metodunu kullanıyoruz.
            // Burada "where" filtresi null veya true şeklinde tüm kayıtları getiriyoruz.
            var kurumEntities = await _kurumRepository.HerSekildeFiltreleAsync(
                select: k => k,
                where: k => true,
                orderBy: null,
                include: query => query.Include(k => k.Paket)
            );

            // Entity'den DTO'ya map işlemi
            var kurumDtos = kurumEntities.Select(k => new KurumList_DTO
            {
                KurumID = k.KurumID,
                KurumAdi = k.KurumAdi,
                PaketBaslangicTarihi = k.PaketBaslangicTarihi,
                PaketBitisTarihi = k.PaketBitisTarihi,
                PaketAktifMi = k.PaketAktifMi,
            }).ToList();

            return kurumDtos;
        }

        public async Task<KurumDetay_DTO> GetKurumByIdAsync(int id)
        {
            // Paket bilgisini de çekebilmek için ilgili entity'i dahil ederek sorguluyoruz.
            var kurumEntities = await _kurumRepository.HerSekildeFiltreleAsync(
                select: k => k,
                where: k => k.KurumID == id,
                orderBy: null,
                include: query => query.Include(k => k.Paket)
            );

            var kurumEntity = kurumEntities.FirstOrDefault();
            if (kurumEntity == null)
                return null;

            // Entity'den detay DTO'suna map işlemi
            var detayDTO = new KurumDetay_DTO
            {
                KurumID = kurumEntity.KurumID,
                KurumAdi = kurumEntity.KurumAdi,
                Adres = kurumEntity.Adres,
                PaketID = kurumEntity.PaketID,
                // Paket bilgilerini daha özel olarak çekebilirsiniz. Örneğin, paket adını almak için:
                PaketBilgi = kurumEntity.Paket.PaketAdi,
                PaketBaslangicTarihi = kurumEntity.PaketBaslangicTarihi,
                PaketBitisTarihi = kurumEntity.PaketBitisTarihi,
                PaketAktifMi = kurumEntity.PaketAktifMi,
                VergiNumarasi = kurumEntity.VergiNumarasi,
                EklenmeTarihi = kurumEntity.EklenmeTarihi,
                GuncellemeTarihi = kurumEntity.GuncellemeTarihi,
                SilmeTarihi = kurumEntity.SilmeTarihi,
                KayitDurumu = kurumEntity.KayitDurumu.HasValue ? kurumEntity.KayitDurumu.Value.ToString() : string.Empty
            };

            return detayDTO;
        }
    }
}