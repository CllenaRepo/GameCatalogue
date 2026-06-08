using GameCatalogue.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogue.API.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                Id = 1,
                Title = "The Legend of Zelda: Breath of the Wild",
                Genre = "Action-Adventure",
                Platform = "Nintendo Switch",
                ReleaseYear = 2017,
                Developer = "Nintendo"
            },
            new Game
            {
                Id = 2,
                Title = "Elden Ring",
                Genre = "Action RPG",
                Platform = "PC / PS5 / Xbox Series X",
                ReleaseYear = 2022,
                Developer = "FromSoftware"
            },
            new Game
            {
                Id = 3,
                Title = "Red Dead Redemption 2",
                Genre = "Action-Adventure",
                Platform = "PC / PS4 / Xbox One",
                ReleaseYear = 2018,
                Developer = "Rockstar Games"
            },
            new Game
            {
                Id = 4,
                Title = "Hades",
                Genre = "Roguelike",
                Platform = "PC / Nintendo Switch",
                ReleaseYear = 2020,
                Developer = "Supergiant Games"
            },
            new Game
            {
                Id = 5,
                Title = "Cyberpunk 2077",
                Genre = "Action RPG",
                Platform = "PC / PS5 / Xbox Series X",
                ReleaseYear = 2020,
                Developer = "CD Projekt Red"
            }
        );
    }
}
