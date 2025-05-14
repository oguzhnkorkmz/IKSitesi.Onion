using IK.Application.Layer.Models.DTO_s.IzinTalebi;
using IK.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.IzinTalebi
{
    public interface IIzinTalebiService
    {
        Task<int> IzinTalebiEkleAsync(IzinTalebiEkle_DTO dto);
        Task<List<IK.CoreLayer.Entities.IzinTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId);

        Task<List<IK.CoreLayer.Entities.IzinTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId);
        Task<bool> OnaylaTalepAsync(int izinTalebiID, int kurumId);
        Task<bool> ReddetTalepAsync(int izinTalebiID, int kurumId);

    }
}
