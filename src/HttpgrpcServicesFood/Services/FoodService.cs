using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Services.Foods.Domain.Models;
using Httpgrpc.Services.Foods.Repositories;

namespace Httpgrpc.Services.Foods.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FoodData>> GetFoodItemsAsync()
        {
            return await _repository.GetFoodAsync();
        }
        public async Task AddFoodItems(FoodData foodData)
        {
            await _repository.AddAsync(foodData);
        }

    }
}