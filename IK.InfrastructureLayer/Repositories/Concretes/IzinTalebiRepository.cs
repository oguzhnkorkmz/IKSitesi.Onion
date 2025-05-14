using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IK.CoreLayer.Enums;
using System.Linq.Expressions;

namespace IK.InfrastructureLayer.Repositories.Concretes
{
    public class IzinTalebiRepository : BaseRepository<IzinTalebi>, IIzinTalebiRepository
    {
        public IzinTalebiRepository(IKDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IzinTalebi> GetIzinTalebiForAvansAndHarcamaAsync(int id)
        {
            return await _table
                .Include(i => i.Personel) // Personel bilgisini de yüklüyoruz.
                .FirstOrDefaultAsync(i => i.IzinTalebiID == id);
        }


    }
}