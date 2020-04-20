using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Httpgrpc.Services.Identity.Domain.Models;
//using Httpgrpc.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.Options;

namespace Httpgrpc.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context; 
        public UserRepository(IOptions<IdentitySettings> settings)
        {
            _context = new UsersContext(settings);
        }

        public async Task<User> GetAsync(Guid id)
        {   
            var filter = Builders<User>.Filter.Eq("Id", id);
            return await _context.User.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(string email)
         {   
            var filter = Builders<User>.Filter.Eq("Email", email);
            return await _context.User.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
            => await _context.User.InsertOneAsync(user);
        public async Task<User> GetUserAsync()
            => await _context.User.AsQueryable().FirstOrDefaultAsync();
    }
}