using ContosoPizza.Helpers;
using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services
{
    public class UserService
    {
        private readonly ApiContext _context;
        private readonly UserRepository _userRepository;
        public UserService(ApiContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers().ToList();
        }

        public User ? GetUser(int id)
        {
            return _userRepository.GetAllUsers().FirstOrDefault(user => user.Id == id);
        }

        public User ? GetUser(string email)
        {
            return _userRepository.GetAllUsers().FirstOrDefault(user => user.Email == email);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        
        public void DeleteUser(int id)
        {
            var user = GetUser(id);

            if (user is null)
            {
                return;
            }

            _context.Users.Remove(user);

            _context.SaveChanges();
        }

        public void UpdateUser(int id, User user)
        {
            var userToUpdate = _context.Users.Find(id);

            if (userToUpdate is null)
            {
                return;
            }

            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Active = user.Active;

            _context.SaveChanges();
        }

        public bool UserExists(string email)
        {
            return _userRepository.GetAllUsers().FirstOrDefault(user => user.Email == email) is not null;
        }
    }
}
