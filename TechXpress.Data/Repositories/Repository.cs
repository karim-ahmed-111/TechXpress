using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Context;

namespace TechXpress.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TechXpressContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(TechXpressContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }



        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }


        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
