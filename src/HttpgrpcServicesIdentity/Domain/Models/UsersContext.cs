namespace Httpgrpc.Services.Identity.Domain.Models
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class UsersContext
    {
        private readonly IMongoDatabase _database = null;

        public UsersContext(IOptions<IdentitySettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }      
    }
}
