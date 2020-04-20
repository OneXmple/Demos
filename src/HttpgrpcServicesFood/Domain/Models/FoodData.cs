using System;
using System.Collections.Generic;
using System.Linq;
using Httpgrpc.Common.Exceptions;

namespace Httpgrpc.Services.Foods.Domain.Models
{
    public class FoodData
    {
        public Guid Id { get; protected set; }
        public List<string> Name { get; protected set; }
        public string Description { get; protected set; }
        public string RestaurantName { get; protected set; }

        protected FoodData()
        {
        }

        public FoodData(List<string> name, string restaurantName, string description = "n/a")
        {
            if (string.IsNullOrWhiteSpace(name.FirstOrDefault()))
            {
                throw new HttpgrpcException("empty_food_name",
                    "Food name can not be empty.");
            }
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            RestaurantName = restaurantName;
        }
    }
}