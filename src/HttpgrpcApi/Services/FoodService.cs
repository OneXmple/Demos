using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpgrpc.api.Models;
using Newtonsoft.Json;
using Grpc.Net.Client;
using HttpgrpcServices;
using Microsoft.Extensions.Logging;

namespace httpgrpc.api.Services
{
    public class FoodService : IFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly string _restaurantUrl;
        private readonly string _foodUrl;
        private readonly ILogger<FoodService> _logger;

        public FoodService(HttpClient httpClient, ILogger<FoodService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _restaurantUrl = "http://localhost:27608";
            _foodUrl = "https://localhost:44347";
        }

        private async Task<List<RestaurantResponse>> GetRestaurants()
        {
            var responseString = await _httpClient.GetStringAsync($"{_restaurantUrl}/restaurant");

            return JsonConvert.DeserializeObject<List<RestaurantResponse>>(responseString);
        }

        public async Task CreateFoodItems(FoodItemRequest foodItemRequest)
        {
            var restaurantList = await GetRestaurants();

            if (restaurantList == null || restaurantList.Count < 1) throw new ArgumentNullException();

            var restaurant = restaurantList.Find(a => a.Name == foodItemRequest.RestaurantName);

            if (restaurant == null) throw new ArgumentNullException("Restaurant has to be an existing Restaurant"); 

            var foodContent = new StringContent(JsonConvert.SerializeObject(foodItemRequest), System.Text.Encoding.UTF8, "application/json");

            var responseString = await _httpClient.PostAsync($"{_foodUrl}/food", foodContent);
        }

        public async Task CheckHealth()
        {
            await _httpClient.GetAsync($"{_foodUrl}/food/health");
        }

        public async Task CreateGrpcFoodRequestAsync(FoodItemRequest foodItems)
        {
            _logger.LogInformation("grpc client created");

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:44347");
            var client = new FoodyService.FoodyServiceClient(channel);

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

            _logger.LogError("grpc response {@response}", response);
        }
    }
}
