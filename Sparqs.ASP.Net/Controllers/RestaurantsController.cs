using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparqs.ASP.Net.Models;

namespace Sparqs.ASP.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private RestaurantContext _context;

        private WebApiService WebApiService;

        public RestaurantsController(RestaurantContext context)
        {
            _context = context;
            WebApiService = new WebApiService();
        }

        // GET: api/Restaurants
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants()
        { 
            return WebApiService.GetRestaurants();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public Restaurant GetRestaurantById(int id)
        {
            return WebApiService.GetRestaurantById(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void SaveRestaurant(Restaurant restaurant)
        {
            WebApiService.SaveRestaurant(restaurant);
        }
    }
}
