using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Controllers
{
    [ApiController]
    [Route("[controller]/{input}")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string input)
        {
            var order = new Order(input);

            if (!string.IsNullOrEmpty(order.Error))
            {
                ModelState.AddModelError(
                    nameof(input),
                    order.Error
                );
                return BadRequest(ModelState);
            }

            return Ok(order.GetOutput());
        }

    }
}
