using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        public Task<IReadOnlyCollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(Guid id);
        public Task DeleteByIdAsync(Guid id);
        public Task UpdateItemAsync(Guid id, T entity);
        public Task CreateAsync(T entity);
    }
}
