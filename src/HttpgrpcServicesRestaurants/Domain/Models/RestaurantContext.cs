namespace Httpgrpc.Services.Restaurants.Domain.Models
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class RestaurantContext
    {
        private readonly IMongoDatabase _database = null;

        public RestaurantContext(IOptions<RestaurantSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Restaurant> Restaurant
        {
            get
            {
                return _database.GetCollection<Restaurant>("Restaurant");
            }
        }      
    }
}
