namespace Httpgrpc.Services.Foods.Domain.Models
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class FoodsContext
    {
        private readonly IMongoDatabase _database = null;

        public FoodsContext(IOptions<FoodSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<FoodData> FoodData
        {
            get
            {
                return _database.GetCollection<FoodData>("FoodData");
            }
        }      
    }
}
