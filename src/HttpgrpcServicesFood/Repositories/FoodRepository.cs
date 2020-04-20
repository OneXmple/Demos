using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;
using Httpgrpc.Services.Foods.Domain.Models;
using System.Collections.Generic;

namespace Httpgrpc.Services.Foods.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodsContext _context; 
        public FoodRepository(IOptions<FoodSettings> settings)
        {
            _context = new FoodsContext(settings);
        }

        public async Task<FoodData> GetAsync(Guid id)
        {   
            var filter = Builders<FoodData>.Filter.Eq("Id", id);
            return await _context.FoodData.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<FoodData>> GetFoodAsync()
           => await _context.FoodData.AsQueryable().ToListAsync();
        

        public async Task AddAsync(FoodData foodData)
           => await _context.FoodData.InsertOneAsync(foodData);

    }
}