using System.Threading.Tasks;
using Httpgrpc.Common.Commands;
using Httpgrpc.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Httpgrpc.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("health")]
        public IActionResult Get() => Ok();

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
        
        [HttpGet("")]
        public async Task<IActionResult> GetUser() => Json(await _userService.GetUserAsync());
    }
}