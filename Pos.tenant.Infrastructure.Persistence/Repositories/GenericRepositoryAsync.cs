using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T,TKey> : IGenericRepositoryAsync<T, TKey> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            return await _dbSet.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        { 
            return await _dbSet.CountAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }
    }
}
