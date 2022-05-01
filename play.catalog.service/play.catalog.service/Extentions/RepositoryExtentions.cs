using Entites;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using play.catalog.service.Settings;
using Repositories;

namespace play.catalog.service.Extentions
{
    public static class RepositoryExtentions
    {
        public static IServiceCollection AddMongo(this IServiceCollection Services)
        {
            // if saw a Guid type convert it to string
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            // if saw a DataTimeOffset type convert it to string
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            Services.AddSingleton(ServiceProvider =>
            {
                var Configuration = ServiceProvider.GetService<IConfiguration>();

                var serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

                var mongoClient = new MongoClient(mongoSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return Services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection Services,string collectionName)
            where T : IEntity
        {
            Services.AddSingleton<IRepository<T>>(ServiceProvider =>
            {
                // get an instance of a service which is already registerd in services
                var database = ServiceProvider.GetService<IMongoDatabase>();
                return new RepositoryMongoDb<T>(database, collectionName);
            });

            return Services;
        }
    }
}
