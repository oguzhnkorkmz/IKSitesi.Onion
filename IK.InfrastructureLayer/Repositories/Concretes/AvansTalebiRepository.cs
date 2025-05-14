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
    public class AvansTalebiRepository:BaseRepository<AvansTalebi>,IAvansTalebiRepository
    {
        public AvansTalebiRepository(IKDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<AvansTalebi> GetIzinTalebiForAvansAndHarcamaAsync(int id)
        {
            return await _table
                .Include(i => i.Personel) // Personel bilgisini de yüklüyoruz.
                .FirstOrDefaultAsync(i => i.AvansTalebiID == id);
        }
    }
}
