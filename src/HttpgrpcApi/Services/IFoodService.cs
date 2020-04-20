using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using httpgrpc.api.Models;

namespace httpgrpc.api.Services
{
    public interface IFoodService
    {
        Task CreateFoodItems(FoodItemRequest foodItemRequest);
        Task CheckHealth();
        Task CreateGrpcFoodRequestAsync(FoodItemRequest foodItems);
    }
}
