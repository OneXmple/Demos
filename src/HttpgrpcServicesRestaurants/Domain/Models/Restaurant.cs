using System;
using Httpgrpc.Common.Exceptions;

namespace Httpgrpc.Services.Restaurants.Domain.Models
{
    public class Restaurant
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }

        protected Restaurant()
        {
        }

        public Restaurant(string name, string address, string city, string state)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new HttpgrpcException("empty_restaurant_name", 
                    "Restaurant name can not be empty.");
            }        
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            City = city;
            State = state;
        }
    }
}