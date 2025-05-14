using IK.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Repositories.Abstract
{
    public interface IHarcamaTalebiRepository:IBaseRepository<HarcamaTalebi>
    {
        Task<HarcamaTalebi> GetIzinTalebiForAvansAndHarcamaAsync(int id);
    }
}
