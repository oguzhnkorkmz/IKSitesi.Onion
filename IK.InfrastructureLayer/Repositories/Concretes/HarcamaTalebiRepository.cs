using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Repositories.Concretes
{
    public class HarcamaTalebiRepository : BaseRepository<HarcamaTalebi>,IHarcamaTalebiRepository
    {
        public HarcamaTalebiRepository(IKDBContext dbContext) : base(dbContext) {
        }

        public async Task<HarcamaTalebi> GetIzinTalebiForAvansAndHarcamaAsync(int id)
        {
            return await _table
                .Include(i => i.Personel) // Personel bilgisini de yüklüyoruz.
                .FirstOrDefaultAsync(i => i.HarcamaTalebiID == id);
        }
    }

}
