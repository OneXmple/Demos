using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Services.Restaurants.Domain.Models;
using Httpgrpc.Services.Restaurants.Repositories;

namespace Httpgrpc.Services.Restaurants.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;

        }

        public async Task<Restaurant> GetRestaurantByIdAsync(string id)
        {          
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Restaurant> GetRestaurantByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await _repository.GetRestaurantsAsync();
        }

        public async Task AddRestaurant(Restaurant restaurant)
        {
            await _repository.AddAsync(restaurant);
        }
    }
}