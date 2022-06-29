using ContosoPizza.Models;
using ContosoPizza.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Repositories
{
    public class PizzaRepository
    {
        private readonly ApiContext _context;
        public PizzaRepository(ApiContext context)
        {
            _context = context;
        }

        public IQueryable<Pizza> GetAll()
        {
            return _context.Pizzas;
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
                     .Where(pizza => pizza.Ingredients!.All(ingredient => ingredient.Name!.ToLower() == parameters.Ingredient.ToLower()));
            }

            if (parameters.Name is not null)
            {
                result = result
                     .Where(pizza => pizza.Name!.ToLower().Contains(parameters.Name.ToLower()));
            }

            if (parameters.Page is not null && parameters.MaxItems is not null)
            {
                if (parameters.Page == 1)
                {
                    result = result.Take((int)parameters.MaxItems);
                } else if (parameters.Page > 1)
                {
                    result = result.Skip(((int)parameters.Page * (int)parameters.MaxItems) - (int)parameters.MaxItems).Take((int)parameters.MaxItems);
                }
            }

            return result;
        }
    }
}
