using GameCatalogue.API.Models;

namespace GameCatalogue.API.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<Game> CreateAsync(Game game);
    Task<Game?> UpdateAsync(int id, Game game);
    Task<bool> DeleteAsync(int id);
}
