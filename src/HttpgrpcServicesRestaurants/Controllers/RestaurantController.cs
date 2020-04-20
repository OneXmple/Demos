using System.Threading.Tasks;
using httpgrpc.services.restaurants.Command;
using Httpgrpc.Services.Restaurants.Domain.Models;
using Httpgrpc.Services.Restaurants.Services;
using Microsoft.AspNetCore.Mvc;

namespace Httpgrpc.Services.Restaurants.Controllers
{
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("health")]
        public IActionResult Get() => Ok();

        [HttpGet("")]
        public async Task<IActionResult> GetRestaurant() => Json(await _restaurantService.GetRestaurantsAsync());

        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<IActionResult> GetRestaurant(string id) => Json(await _restaurantService.GetRestaurantByIdAsync(id));

        [HttpGet("name/{name}", Name = "GetRestaurantName")]
        public async Task<IActionResult> GetRestaurantName(string name) => Json(await _restaurantService.GetRestaurantByNameAsync(name));


        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]AddRestaurantCommand restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Name)) return BadRequest();

            await _restaurantService.AddRestaurant(new Restaurant(restaurant.Name,restaurant.Address, restaurant.City, restaurant.State));

            return Accepted();
        }
    }
}