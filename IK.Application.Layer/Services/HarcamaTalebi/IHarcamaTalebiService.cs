using IK.Application.Layer.Models.DTO_s.HarcamaTalebi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.HarcamaTalebi
{
    public interface IHarcamaTalebiService
    {
        Task<int> EkleHarcamaTalebiAsync(HarcamaTalebiEkle_DTO dto);
        Task<List<IK.CoreLayer.Entities.HarcamaTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId);

        Task<List<IK.CoreLayer.Entities.HarcamaTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId);
        Task<bool> OnaylaTalepAsync(int HarcamaTalebiID, int kurumId);
        Task<bool> ReddetTalepAsync(int HarcamaTalebiID, int kurumId);

    }
}
