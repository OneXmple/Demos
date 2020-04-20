namespace Httpgrpc.Services.Foods.Domain.Models
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FoodsContextSeed
    {
        private static FoodsContext ctx;
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            //var config = applicationBuilder
            //    .ApplicationServices.GetRequiredService<IOptions<FoodSettings>>();

            //ctx = new FoodsContext(config);

            //if (!ctx.FoodData.Database.GetCollection<FoodData>(nameof(FoodData)).AsQueryable().Any())
            //{
                //await SetFoods();
            //}
        }

        static async Task SetFoods()
        {
            //var user = new User("email@newguy.com", "New Guy");

            //ctx.FoodData.InsertOneAsync(user);
        }
    }
}
