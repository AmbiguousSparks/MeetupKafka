using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Order.Domain.Models;
using System;

namespace Order.Infra.Mongo
{
    public class MongoContext : IMongoContext
    {
        private const string DATABASE_NAME = "meetup";
        private readonly IMongoDatabase _mongoDb;
        public MongoContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string can not be null", nameof(connectionString));
            Register();
            var mongoClient = new MongoClient(connectionString);
            _mongoDb = mongoClient.GetDatabase(DATABASE_NAME);
        }
        private static void Register()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };

            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
            BsonSerializer.RegisterIdGenerator(typeof(Guid), CombGuidGenerator.Instance);
            BsonClassMap.RegisterClassMap<Invoice>(cm =>
            {
                cm.AutoMap();
            });
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongoDb.GetCollection<T>(name);
        }
    }
}
