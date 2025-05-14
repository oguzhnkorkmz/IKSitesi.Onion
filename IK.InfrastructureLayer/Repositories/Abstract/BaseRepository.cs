using IK.CoreLayer.Abstracts;
using IK.CoreLayer.Enums;
using IK.CoreLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IK.InfrastructureLayer.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected IKDBContext _dbContext;
        protected DbSet<TEntity> _table;

        protected BaseRepository(IKDBContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }

        public virtual async Task<int> EkleAsync(TEntity entity)
        {
            entity.EklenmeTarihi = DateTime.Now;
            entity.KayitDurumu = KayitDurumu.KayitEkleme;

            await _table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return -1;
        }

        public async Task GuncelleAsync(TEntity entity)
        {
            entity.GuncellemeTarihi = DateTime.Now;
            entity.KayitDurumu = KayitDurumu.KayitGuncelle;

            _table.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SilAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            if (entity != null)
            {
                entity.SilmeTarihi = DateTime.Now;
                entity.KayitDurumu = KayitDurumu.KayitSil;

                _table.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity> AraAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<List<TEntity>> ListeleAsync()
        {
            return await _table.Where(x => x.KayitDurumu != KayitDurumu.KayitSil).ToListAsync();
        }


        public async Task<IEnumerable<TResult>> HerSekildeFiltreleAsync<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _table.AsNoTracking();
            if (where != null)
                query = query.Where(where);
            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();
            else
                return await query.Select(select).ToListAsync();
        }
    }
}