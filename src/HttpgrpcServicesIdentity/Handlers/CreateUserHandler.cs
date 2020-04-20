using System;
using System.Threading.Tasks;
using Httpgrpc.Common.Commands;
using Httpgrpc.Common.Events;
using Httpgrpc.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Httpgrpc.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient,
            IUserService userService, 
            ILogger<CreateUser> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: '{command.Email}' with name: '{command.Name}'.");
           
            await _userService.RegisterAsync(command.Email, command.Password, command.Name);
            //await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
            _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Name}'.");

            return;           
        }
    }
}