using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Services;
using ContosoPizza.Models;
using ContosoPizza.Helpers;
namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ApiContext _context;
        private PizzaService pizzaService;
        public PizzaController(ApiContext context)
        {
            _context = context;

            pizzaService = new PizzaService(context);
        }

        [HttpGet]
        public IQueryable<Pizza> GetItems([FromQuery] QueryParameters parameters)
        {
            return pizzaService.GetFiltered(parameters);
        }

        //[HttpGet("filter")]
        //public IQueryable<Pizza> GetFiltered([FromQuery] QueryParameters parameters)
        //{
        //    return pizzaService.GetFiltered(parameters);
        //}

        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = pizzaService.Get(id);
            
            if (pizza is null)
            {
                return NotFound();
            }

            return pizza;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            pizzaService.Add(pizza);

            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            var pizzaToUpdate = pizzaService.Get(id);

            if (pizzaToUpdate is null)
            {
                return NotFound();
            }

            pizzaService.Update(pizza);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = pizzaService.Get(id);

            if (pizza is null)
            {
                return NotFound();
            }

            pizzaService.Delete(id);

            return NoContent();
        }
    }

    
}
