using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.services.food.Commands
{
    public class AddFoodItemsCommand
    {
        public List<Items> FoodItems { get; set; }
        public string Description { get; set; }
        public string RestaurantName { get; set; }
    }

    public class Items
    {
        public string Name { get; set; }
    }
}
