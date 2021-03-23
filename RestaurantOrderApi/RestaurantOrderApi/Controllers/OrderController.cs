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
                return FormBadRequest(nameof(input), order.Error);
            }

            return Ok(order.GetOutput());
        }

        private IActionResult FormBadRequest(string key, string error)
        {
            ModelState.AddModelError(
                key,
                error
            );
            return BadRequest(ModelState);
        }
    }
}
