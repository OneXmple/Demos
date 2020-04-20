using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Httpgrpc.Services.Foods.Domain.Models;

namespace Httpgrpc.Services.Foods.Repositories
{
    public interface IFoodRepository
    {
        Task<List<FoodData>> GetFoodAsync();
        Task AddAsync(FoodData foodData);
    }
}