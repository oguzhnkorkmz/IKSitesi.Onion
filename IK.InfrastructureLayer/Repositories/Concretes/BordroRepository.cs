using IK.CoreLayer.Entities;
using IK.InfrastructureLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Repositories.Concretes
{
    public class BordroRepository : BaseRepository<Bordro>
    {
        public BordroRepository(IKDBContext dbContext) : base(dbContext) { 
        }

    }
}