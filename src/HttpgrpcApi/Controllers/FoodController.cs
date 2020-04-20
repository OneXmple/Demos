using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using httpgrpc.api.Models;
using httpgrpc.api.Services;
//using Actio.Api.Repositories;
using Httpgrpc.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Httpgrpc.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class FoodController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly IFoodService _service;

        public FoodController(IBusClient busClient, IFoodService service)
        {
            _busClient = busClient;
            _service = service;          
        }

        [HttpGet("health")]
        public async Task<IActionResult> Get()
        {
            var userId = Request.HttpContext.User.Identity.Name;
            return Ok();
        }

        // Post using Http Client
        [HttpPost("")]
        public async Task<IActionResult> PostFood([FromBody]FoodItemRequest request)
        {            
            if (request == null) return BadRequest();

            await _service.CreateFoodItems(request);

            return Accepted();
        }

        // Post using Grpc Client
        [HttpPost("grpc")]
        public async Task<IActionResult> PostFoodGrpc([FromBody]FoodItemRequest request)
        {
            if (request == null) return BadRequest();

            await _service.CreateGrpcFoodRequestAsync(request);

            return Accepted();
        }
    }
}