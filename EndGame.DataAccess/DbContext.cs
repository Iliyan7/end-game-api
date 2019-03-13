using EndGame.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EndGame.DataAccess
{
    public class EndGameContext : DbContext
    {
        public EndGameContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameImage> GameImages { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameImage>()
                .HasKey(gi => new { gi.GameId, gi.ImageId });
        }
    }
}
