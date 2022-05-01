using Entites;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories
{
    public class RepositoryMongoDb<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;

        public RepositoryMongoDb(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        { 
            return await dbCollection.Find(T => T.Id == id).SingleOrDefaultAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(T => T.Id == id);
        }

        public async Task UpdateItemAsync(Guid id,T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
             
            await dbCollection.ReplaceOneAsync(T=>T.Id==id, entity);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbCollection.InsertOneAsync(entity);
        }
    }
}