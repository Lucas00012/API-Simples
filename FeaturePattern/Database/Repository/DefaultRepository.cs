using API.Database.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Database.Repository
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private AppDbContext _context;
        public DefaultRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public async Task Add(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(T obj)
        {
            _dbSet.Remove(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }
    }
}
