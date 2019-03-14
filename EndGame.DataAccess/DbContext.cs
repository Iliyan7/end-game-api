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

        public DbSet<Game> Games { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<GameImage> GameImages { get; set; }

        public DbSet<GamePlatform> GamePlatforms { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscriber>()
               .HasIndex(u => u.Email)
               .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<GameGenre>()
               .HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.Entity<GameImage>()
                .HasKey(gi => new { gi.GameId, gi.ImageId });

            modelBuilder.Entity<GamePlatform>()
               .HasKey(gp => new { gp.GameId, gp.PlatformId });

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
