using System.Threading.Tasks;
using Httpgrpc.Common.Auth;
using Httpgrpc.Services.Identity.Domain.Models;

namespace Httpgrpc.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
         Task<JsonWebToken> LoginAsync(string email, string password);

         Task<User> GetUserAsync();
    }
}