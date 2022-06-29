using ContosoPizza.Models;
using ContosoPizza.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Repositories
{
    public class UserRepository
    {
        private readonly ApiContext _context;
        public UserRepository(ApiContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }
    }
}
