using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ContosoPizza.Helpers
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Pizza> Pizzas { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
    }
}
