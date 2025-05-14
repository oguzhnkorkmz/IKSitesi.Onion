using IK.CoreLayer.Entities;
using IK.CoreLayer.Repositories.Abstract;
using IK.InfrastructureLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Repositories.Concretes
{
    public class PersonelRepository:BaseRepository<Personel>,IPersonelRepository
    {
        public PersonelRepository(IKDBContext dbContext) : base(dbContext)
        {
        }
    }
}
