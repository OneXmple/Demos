using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.services.restaurants.Queries
{
    public class GetRestaurantNameQuery
    {
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
    }
}
