using Gesd.Data.Context;
using Gesd.Features.Contrats.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
         where T : class
    {
        private readonly GesdContext _dbContext;

        public GenericRepository(GesdContext context)
        {
            _dbContext = context;
        }

        public async Task<T> Add(T entite)
        {
            await _dbContext.AddAsync(entite);
            await _dbContext.SaveChangesAsync();
            return entite;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entite)
        {
            try
            {
                _dbContext.Entry(entite).State = EntityState.Modified;
                return entite;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<bool> Delete(T entite)
        {
            _dbContext.Set<T>().Remove(entite);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(Guid id)
        {
            var entity = await Get(id);
            return entity != null;
        }
    }
}
