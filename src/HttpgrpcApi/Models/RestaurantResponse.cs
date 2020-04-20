using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.api.Models
{
    public class RestaurantResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
