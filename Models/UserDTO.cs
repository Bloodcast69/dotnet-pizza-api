namespace ContosoPizza.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string ? Email { get; set; }

        public bool ? Active { get; set; }
        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Active = user.Active;
        }
    }
}
