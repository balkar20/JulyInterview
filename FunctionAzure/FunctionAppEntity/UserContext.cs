using Microsoft.EntityFrameworkCore;

namespace FunctionAppEntity
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public int Age { get; set; }
    }
}