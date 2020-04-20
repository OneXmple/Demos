namespace Httpgrpc.Services.Restaurants.Domain.Models
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RestaurantContextSeed
    {
        private static RestaurantContext ctx;
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var config = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<RestaurantSettings>>();

            ctx = new RestaurantContext(config);

            if (!ctx.Restaurant.Database.GetCollection<Restaurant>(nameof(Restaurant)).AsQueryable().Any())
            {
                await SetRestaurants();
            }
        }

        static async Task SetRestaurants()
        {
            var rests = new List<Restaurant>() { new Restaurant("Blumpies", "1909 Fells Touch Pl", "Oranges", "FF"), new Restaurant("Hilpes", "104 Makes Street", "Pearls", "UI") };

            await ctx.Restaurant.InsertManyAsync(rests);
        }
    }
}
