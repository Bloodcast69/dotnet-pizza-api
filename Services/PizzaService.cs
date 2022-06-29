using ContosoPizza.Helpers;
using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly ApiContext _context;
        private readonly PizzaRepository _pizzaRepository;
        public PizzaService(ApiContext context)
        {
            _context = context;
            _pizzaRepository = new PizzaRepository(_context);
        }
       
        public List<Pizza> GetFiltered(QueryParameters parameters)
        {
            return _pizzaRepository.GetFiltered(parameters).ToList();
        }

        public Pizza ? Get(int id)
        {
            return _pizzaRepository.GetAll().FirstOrDefault(pizza => pizza.Id == id);
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
