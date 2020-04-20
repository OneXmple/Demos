using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Services.Restaurants.Domain.Models;

namespace Httpgrpc.Services.Restaurants.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetByIdAsync(string id);
        Task<Restaurant> GetByNameAsync(string name);
        Task AddAsync(Restaurant restaurant);
        Task<List<Restaurant>> GetRestaurantsAsync();
    }
}