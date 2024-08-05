using Microsoft.EntityFrameworkCore;

namespace FeedBackAdmin.Models
{
    // Чтобы подключиться к базе данных через Entity Framework, необходим контекст данных. 
    // Контекст данных представляет собой класс, производный от класса DbContext.
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
    }
}