using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.api.Models
{
    public class FoodItemRequest
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
