using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DepremVeriProjesi.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoDBService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _collection = database.GetCollection<BsonDocument>(config["MongoDB:CollectionName"]);
        }

        public async Task AddDepremVerisiAsync(BsonDocument document)
        {
            await _collection.InsertOneAsync(document);
        }
    }
}
