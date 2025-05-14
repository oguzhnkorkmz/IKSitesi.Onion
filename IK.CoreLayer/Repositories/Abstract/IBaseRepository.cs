using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK.CoreLayer.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<int> EkleAsync(TEntity entity);
        public Task GuncelleAsync(TEntity entity);

        public Task SilAsync(int id);

        public Task<TEntity> AraAsync(int id);

        public Task<List<TEntity>> ListeleAsync();

        public Task<IEnumerable<TResult>> HerSekildeFiltreleAsync<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            );

       
    }
}