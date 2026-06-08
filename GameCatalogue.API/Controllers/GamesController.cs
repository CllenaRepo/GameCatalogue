using GameCatalogue.API.Models;
using GameCatalogue.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogue.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameRepository _repo;

    public GamesController(IGameRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetAll()
    {
        var games = await _repo.GetAllAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetById(int id)
    {
        var game = await _repo.GetByIdAsync(id);
        return game is null ? NotFound() : Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> Create(Game game)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _repo.CreateAsync(game);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Game>> Update(int id, Game game)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _repo.UpdateAsync(id, game);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
