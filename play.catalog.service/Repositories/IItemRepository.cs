using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IItemRepository
    {
        public Task<IReadOnlyCollection<Item>> GetAllAsync();
        public Task<Item> GetByIdAsync(Guid id);
        public Task DeleteByIdAsync(Guid id);
        public Task UpdateItemAsync(Guid id, Item item);
        public Task CreateAsync(Item item);
    }
}
