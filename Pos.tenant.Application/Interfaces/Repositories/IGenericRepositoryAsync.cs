using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.tenant.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T,TKey> where T : class
    {
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
