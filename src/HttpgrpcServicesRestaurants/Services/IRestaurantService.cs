using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Common.Auth;
using Httpgrpc.Services.Restaurants.Domain.Models;

namespace Httpgrpc.Services.Restaurants.Services
{
    public interface IRestaurantService
    {
         Task<Restaurant> GetRestaurantByIdAsync(string id);
         Task<Restaurant> GetRestaurantByNameAsync(string name);
         Task AddRestaurant(Restaurant restaurant);
         Task<List<Restaurant>> GetRestaurantsAsync();
    }
}