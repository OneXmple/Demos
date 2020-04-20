using httpgrpc.services.food.Commands;
using Httpgrpc.Services.Foods.Domain.Models;
using Httpgrpc.Services.Foods.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Httpgrpc.Services.Foods.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("health")]
        public IActionResult Get()
        {
            var tt = Request.HttpContext.Request.Headers;
            var userId = Request.HttpContext.User.Identity.Name;

            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFood() => Json(await _foodService.GetFoodItemsAsync());

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]AddFoodItemsCommand command)
        {
            List<string> newFoodItems = new List<string>();

            if (command == null) return BadRequest();

            if (command.FoodItems.Count < 1) return BadRequest();

            command.FoodItems.ForEach(a => newFoodItems.Add(a.Name));

            await _foodService.AddFoodItems(new FoodData(newFoodItems, command.RestaurantName, command.Description));

            return Accepted();
        }
    }
}