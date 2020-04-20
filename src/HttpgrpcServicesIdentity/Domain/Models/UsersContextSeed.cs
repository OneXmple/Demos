namespace Httpgrpc.Services.Identity.Domain.Models
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UsersContextSeed
    {
        private static UsersContext ctx;
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var config = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<IdentitySettings>>();

            ctx = new UsersContext(config);

            if (!ctx.User.Database.GetCollection<User>(nameof(User)).AsQueryable().Any())
            {
                await SetUsers();
                // await SetNorthAmerica();
                // await SetSouthAmerica();
                // await SetAfrica();
                // await SetEurope();
                // await SetAsia();
                // await SetAustralia();
                // await SetBarcelonaLocations();
            }
        }

        static async Task SetUsers()
        {
            var user = new User("email@newguy.com", "New Guy");

            await ctx.User.InsertOneAsync(user);
        }
    }
}
