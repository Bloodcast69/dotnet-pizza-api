using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Helpers;

namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService userService;

        public UserController(ApiContext context)
        {
            userService = new UserService(context);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var user = userService.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }

            return new UserDTO(user);
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            var UsersList = new List<UserDTO>();

            userService.GetAllUsers().ForEach(user => UsersList.Add(new UserDTO(user)));

            return UsersList;
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var UserInDb = userService.GetUser(user.Email!);


            if (UserInDb is null)
            {
                return NotFound();
            }

            if (UserInDb.Email != user.Email || UserInDb.Password != user.Password || UserInDb.Active == false)
            {
                return Unauthorized();
            }

            return Ok(new UserDTO(UserInDb));
        }

        [HttpPost("register")]
        public IActionResult CreateUser(UserRegisterDTO user)
        {
            if (user.Password != user.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            if (userService.UserExists(user.Email!))
            {
                return BadRequest("Account for such email already exists");
            }

            var newUser = new User { Email = user.Email, Password = user.Password, Active = false };

            userService.CreateUser(newUser);

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, new UserDTO(newUser));
        }

        [HttpPost("confirm-account")]
        public IActionResult ConfirmAccount(User user)
        {
            var UserInDb = userService.GetUser(user.Email!);


            if (UserInDb is null)
            {
                return NotFound();
            }

            if (UserInDb.Active == true)
            {
                return BadRequest("User is already active");
            }

            UserInDb.Active = true;

            userService.UpdateUser(UserInDb.Id, UserInDb);

            return Ok(new UserDTO(UserInDb));
        }


    [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var userToUpdate = userService.GetUser(id);

            if (userToUpdate is null)
            {
                return NotFound();
            }

            userService.UpdateUser(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = userService.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }

            userService.DeleteUser(id);

            return NoContent();
        }

        [HttpPut("password-reset")]
        public IActionResult ResetPassword(UserRegisterDTO user)
        {
            var userInDb = userService.GetUser(user.Email!);

            if (userInDb is null)
            {
                return NotFound();
            }

            if (user.Password != user.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            userInDb.Password = user.Password;

            userService.UpdateUser(userInDb.Id, new User { Id = userInDb.Id, Email = userInDb.Email, Password = userInDb.Password, Active = userInDb.Active });

            return NoContent();

        }
    }
}
