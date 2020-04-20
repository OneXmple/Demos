using System.Threading.Tasks;
using Httpgrpc.Services.Restaurants.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;

namespace Httpgrpc.Services.Restaurants.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantContext _context; 
        public RestaurantRepository(IOptions<RestaurantSettings> settings)
        {
            _context = new RestaurantContext(settings);
        }

        public async Task<Restaurant> GetByIdAsync(string id)
        {   
            return await _context.Restaurant.Find(document => document.Id == Guid.Parse(id)).FirstOrDefaultAsync();
        }

        public async Task<Restaurant> GetByNameAsync(string name)
         {   
            //var filter = Builders<Restaurant>.Filter.Eq("Name", name);
            return await _context.Restaurant.Find<Restaurant>(a => a.Name == name).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Restaurant restaurant)
            => await _context.Restaurant.InsertOneAsync(restaurant);
        public async Task<List<Restaurant>> GetRestaurantsAsync()
            => await _context.Restaurant.AsQueryable().ToListAsync();
    }
}