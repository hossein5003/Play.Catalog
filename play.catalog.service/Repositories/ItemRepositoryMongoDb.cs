using Entites;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories
{
    public class ItemRepositoryMongoDb : IItemRepository
    {
        private const string collectionName = "item";
        private readonly IMongoCollection<Item> dbCollection;


        public ItemRepositoryMongoDb(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Item> GetByIdAsync(Guid id)
        {
            var x = await dbCollection.Find(item => item.Id == id).SingleOrDefaultAsync();

            return x;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(item => item.Id == id);
        }

        public async Task UpdateItemAsync(Guid id,Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
             
            await dbCollection.ReplaceOneAsync(item=>item.Id==id, item);
        }

        public async Task CreateAsync(Item item)
        {
            if (item==null)
                throw new ArgumentNullException(nameof(item));

            await dbCollection.InsertOneAsync(item);
        }
    }
}