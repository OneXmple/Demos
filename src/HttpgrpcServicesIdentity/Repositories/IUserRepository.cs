using System;
using System.Threading.Tasks;
using Httpgrpc.Services.Identity.Domain.Models;

namespace Httpgrpc.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task<User> GetUserAsync();
    }
}