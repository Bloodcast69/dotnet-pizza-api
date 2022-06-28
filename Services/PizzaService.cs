using ContosoPizza.Helpers;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly ApiContext _context;

        public PizzaService(ApiContext context)
        {
            _context = context;
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizzas.Include(x => x.Ingredients).ToList();
        }

       
        public IQueryable<Pizza> GetFiltered(QueryParameters parameters)
        {
            var result = _context.Pizzas.Include(pizza => pizza.Ingredients).AsQueryable();


            if (parameters.IsGlutenFree is not null)
            {
                result = result
                      .Where(pizza => pizza.IsGlutenFree == parameters.IsGlutenFree);
                    

            }

            if (parameters.Ingredient is not null)
            {
                result = result
                     .Where(pizza => pizza.Ingredients.All(ingredient => ingredient.Name.ToLower() == parameters.Ingredient.ToLower()));
            }

            if (parameters.Name is not null)
            {
                result = result
                     .Where(pizza => pizza.Name.ToLower().Contains(parameters.Name.ToLower()));
            }

            return result;
        }

        public Pizza ? Get(int id)
        {
            return _context.Pizzas.FirstOrDefault(pizza => pizza.Id == id);
        }

        public void Add(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pizza = Get(id);

            if (pizza is null)
            {
                return;
            }

            _context.Pizzas.Remove(pizza);

            _context.SaveChanges();
        }

        public void Update(Pizza pizza)
        {
            var pizzaToUpdate = _context.Pizzas.Find(pizza.Id);

            if (pizzaToUpdate is null)
                return;

            pizzaToUpdate.Name = pizza.Name;
            pizzaToUpdate.IsGlutenFree = pizza.IsGlutenFree;

            _context.SaveChanges();
        }
    }
}
