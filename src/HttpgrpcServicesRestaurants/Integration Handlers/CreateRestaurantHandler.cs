using httpgrpc.common.Commands;
using Httpgrpc.Common.Commands;
using Httpgrpc.Services.Restaurants.Domain.Models;
using Httpgrpc.Services.Restaurants.Repositories;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace httpgrpc.services.restaurants.Integration_Handlers
{
    public class CreateRestaurantHandler : ICommandHandler<CreateRestaurant>
    {
        private readonly IRestaurantRepository _repository;

        public CreateRestaurantHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateRestaurant command)
        {
            var newRestaurant = new Restaurant(command.Name, command.Address, command.City, command.State);

            await _repository.AddAsync(newRestaurant);
        }
    }
}
