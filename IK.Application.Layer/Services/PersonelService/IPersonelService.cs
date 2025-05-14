using IK.Application.Layer.Models.DTO_s.Personel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.Application.Layer.Services.PersonelService
{
    public interface IPersonelService
    {
        Task CreatePersonelAsync(PersonelEkle_DTO dto);
    }
}
