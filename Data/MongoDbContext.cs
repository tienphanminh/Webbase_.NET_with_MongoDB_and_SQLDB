using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebConnectMongoDBAndSQL.Model;

namespace WebConnectMongoDBAndSQL.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            // Retrieve MongoDB connection string from appsettings.json
            string connectionString = configuration.GetConnectionString("MongoDBConnection");

            // Create MongoClient
            var client = new MongoClient(connectionString);

            // Access database
            _database = client.GetDatabase("NhapDatabase");
        }

        // Add properties for each collection you want to interact with
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    }

}
