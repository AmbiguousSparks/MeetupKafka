using MongoDB.Driver;

namespace Order.Infra.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
