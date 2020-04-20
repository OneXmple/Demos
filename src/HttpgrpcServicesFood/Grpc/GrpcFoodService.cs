using Grpc.Core;
using Httpgrpc.Services.Foods.Domain.Models;
using Httpgrpc.Services.Foods.Repositories;
using HttpgrpcServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.services.food.Grpc
{
    public class GrpcFoodService : FoodyService.FoodyServiceBase
    {
        private readonly IFoodRepository _repository;
        private readonly ILogger<GrpcFoodService> _logger;

        public GrpcFoodService(IFoodRepository repository, ILogger<GrpcFoodService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public override async Task<FoodResponse> CreateFood(FoodRequest request, ServerCallContext context)
        {
            List<string> items = new List<string>();

            _logger.LogInformation("Begin grpc call from method {Method} for Foody Restaurant {RestaurantName}", context.Method, request.Foods.RestaurantName);

            request.Foods.FoodItems.ToList().ForEach(a => items.Add(a.Name));

            var foodData = new FoodData(items, request.Foods.RestaurantName, request.Foods.Description);

            if (foodData != null)
            {
                await _repository.AddAsync(foodData);

                context.Status = new Status(StatusCode.OK, "Food created successfulyl.");

                _logger.LogInformation("Grpc call created successfully for Restaurant {RestaurantName}", request.Foods.RestaurantName);

                return new FoodResponse { Message = "Food Created" };
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, "Error creating food.");
            }

            return new FoodResponse { Message = "Food Error." };
        }
    }
}
