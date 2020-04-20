using System.Threading.Tasks;
using httpgrpc.common.Commands;
using Httpgrpc.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Httpgrpc.Api.Controllers
{
    [Route("[controller]")]
    public class RestaurantController: Controller
    {
        private readonly IBusClient _busClient;

        public RestaurantController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateRestaurant command)
        {
            await _busClient.PublishAsync(command);

            return Accepted();
        }
    }
}