using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Common.Auth;
using Httpgrpc.Services.Foods.Domain.Models;

namespace Httpgrpc.Services.Foods.Services
{
    public interface IFoodService
    {
        Task<List<FoodData>> GetFoodItemsAsync();
        Task AddFoodItems(FoodData foodData);
    }
}