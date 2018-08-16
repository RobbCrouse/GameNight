using Microsoft.EntityFrameworkCore;

namespace gamenight.Models
{
    public class GameNightContext : DbContext
    {
        public GameNightContext(DbContextOptions<GameNightContext> options) : base(options){}

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Joiner> Joiners { get; set; }

    }
}