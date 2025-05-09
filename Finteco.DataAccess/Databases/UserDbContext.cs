using Finteco_Core.Domain.Users;
using Finteco_Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Finteco.DataAccess.Databases
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options): base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDatabase");
        }
        public DbSet<User> Users { get; set; }
    }
}
