using GameCatalogue.API.Controllers;
using GameCatalogue.API.Models;
using GameCatalogue.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GameCatalogue.Tests;

public class GamesControllerTests
{
    private readonly Mock<IGameRepository> _repoMock;
    private readonly GamesController _controller;

    public GamesControllerTests()
    {
        _repoMock = new Mock<IGameRepository>();
        _controller = new GamesController(_repoMock.Object);
    }

    // ── GetAll ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk_WithListOfGames()
    {
        var games = new List<Game>
        {
            new() { Id = 1, Title = "Game A", Genre = "RPG", Platform = "PC", ReleaseYear = 2020 },
            new() { Id = 2, Title = "Game B", Genre = "Action", Platform = "PS5", ReleaseYear = 2022 }
        };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(games);

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<IEnumerable<Game>>(ok.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithEmptyList_WhenNoGamesExist()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Game>());

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<IEnumerable<Game>>(ok.Value);
        Assert.Empty(returned);
    }

    // ── GetById ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetById_ReturnsOk_WhenGameExists()
    {
        var game = new Game { Id = 1, Title = "Elden Ring", Genre = "RPG", Platform = "PC", ReleaseYear = 2022 };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(game);

        var result = await _controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<Game>(ok.Value);
        Assert.Equal("Elden Ring", returned.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenGameDoesNotExist()
    {
        _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Game?)null);

        var result = await _controller.GetById(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    // ── Create ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithNewGame()
    {
        var newGame = new Game { Title = "Hades", Genre = "Roguelike", Platform = "PC", ReleaseYear = 2020 };
        var saved = new Game { Id = 10, Title = "Hades", Genre = "Roguelike", Platform = "PC", ReleaseYear = 2020 };
        _repoMock.Setup(r => r.CreateAsync(newGame)).ReturnsAsync(saved);

        var result = await _controller.Create(newGame);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(_controller.GetById), created.ActionName);
        var returned = Assert.IsType<Game>(created.Value);
        Assert.Equal(10, returned.Id);
    }

    // ── Update ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Update_ReturnsOk_WhenGameExists()
    {
        var update = new Game { Id = 1, Title = "Updated Title", Genre = "RPG", Platform = "PC", ReleaseYear = 2021 };
        _repoMock.Setup(r => r.UpdateAsync(1, update)).ReturnsAsync(update);

        var result = await _controller.Update(1, update);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<Game>(ok.Value);
        Assert.Equal("Updated Title", returned.Title);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenGameDoesNotExist()
    {
        var update = new Game { Title = "Ghost", Genre = "Action", Platform = "PC", ReleaseYear = 2021 };
        _repoMock.Setup(r => r.UpdateAsync(99, update)).ReturnsAsync((Game?)null);

        var result = await _controller.Update(99, update);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    // ── Delete ────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenGameExists()
    {
        _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenGameDoesNotExist()
    {
        _repoMock.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

        var result = await _controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
