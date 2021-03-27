using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sparqs.ASP.Net.Models;

namespace Sparqs.ASP.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private RestaurantContext _context;

        private WebApiConnection apiConnection;

        public RestaurantsController(RestaurantContext context)
        {
            _context = context;
            apiConnection = new WebApiConnection();
        }

        // GET: api/Restaurants
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants()
        { 
            return apiConnection.readRestaurants();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(long id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }
    }
}
