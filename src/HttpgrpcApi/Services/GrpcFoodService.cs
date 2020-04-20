using HttpgrpcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Net.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using httpgrpc.api.Models;

namespace httpgrpc.api.Services
{
    public class GrpcFoodService
    {
        private readonly ILogger<GrpcFoodService> _logger;
        public GrpcFoodService(ILogger<GrpcFoodService> logger)
        {
            _logger = logger;
        }

        public async Task CreateFoodRequestAsync(FoodItemRequest foodItems)
        {

            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:48773");
            var client = new FoodyService.FoodyServiceClient(channel);

            _logger.LogDebug("grpc client created");

            var brequest = new FoodRequest();
            brequest.Foods = new Foods
            {
                Description = foodItems.Description,
                RestaurantName = foodItems.RestaurantName
            };

            var request = new FoodRequest
            {
                Foods = new Foods
                {
                    Description = foodItems.Description,
                    RestaurantName = foodItems.RestaurantName
                }
            };           

            foodItems.FoodItems.ForEach(a => request.Foods.FoodItems.Add(
                new HttpgrpcServices.Items { Name = a.Name })
            );

            var response = await client.CreateFoodAsync(request);

            _logger.LogDebug("grpc response {@response}", response);
        }
            
    }
}
