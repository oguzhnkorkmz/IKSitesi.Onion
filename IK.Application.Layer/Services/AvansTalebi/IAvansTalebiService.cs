using IK.Application.Layer.Models.DTO_s.AvansTalebi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.AvansTalebi
{
    public interface IAvansTalebiService
    {
        Task<int> AvansTalebiEkle(AvansTalebiEkle_DTO dto);
        Task<List<IK.CoreLayer.Entities.AvansTalebi>> GetAllTaleplerByKurumIDAsync(int kurumId);

        Task<List<IK.CoreLayer.Entities.AvansTalebi>> GetApprovedTaleplerByPersonelIdAsync(int personelId);
        Task<bool> OnaylaTalepAsync(int AvansTalebiID, int kurumId);
        Task<bool> ReddetTalepAsync(int AvansTalebiID, int kurumId);
    }
}
