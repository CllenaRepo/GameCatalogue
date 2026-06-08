using GameCatalogue.API.Data;
using GameCatalogue.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameCatalogue.API.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameDbContext _context;

    public GameRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
        => await _context.Games.OrderBy(g => g.Title).ToListAsync();

    public async Task<Game?> GetByIdAsync(int id)
        => await _context.Games.FindAsync(id);

    public async Task<Game> CreateAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> UpdateAsync(int id, Game game)
    {
        var existing = await _context.Games.FindAsync(id);
        if (existing is null) return null;

        existing.Title = game.Title;
        existing.Genre = game.Genre;
        existing.Platform = game.Platform;
        existing.ReleaseYear = game.ReleaseYear;
        existing.Developer = game.Developer;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Games.FindAsync(id);
        if (existing is null) return false;

        _context.Games.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
