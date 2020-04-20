using System;
using System.Threading.Tasks;
using Httpgrpc.Common.Auth;
using Httpgrpc.Common.Exceptions;
using Httpgrpc.Services.Identity.Domain.Models;
using Httpgrpc.Services.Identity.Domain.Services;
using Httpgrpc.Services.Identity.Repositories;

namespace Httpgrpc.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository repository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _repository = repository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);
            if (user != null)
            {
                throw new HttpgrpcException("email_in_use",
                    $"Email: '{email}' is already in use.");
            }
            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _repository.GetAsync(email);
            if (user == null)
            {
                throw new HttpgrpcException("invalid_credentials",
                    $"Invalid credentials.");
            }
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new HttpgrpcException("invalid_credentials",
                    $"Invalid credentials.");
            }
        
            return _jwtHandler.Create(user.Id);
        }
         public async Task<User> GetUserAsync()
        {
            return await _repository.GetUserAsync();
        }
    }
}