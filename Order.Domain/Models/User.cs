using Order.Domain.Data;

namespace Order.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
