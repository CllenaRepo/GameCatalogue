using System.ComponentModel.DataAnnotations;

namespace GameCatalogue.API.Models;

public class Game
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Platform { get; set; } = string.Empty;

    [Required, Range(1972, 2026)] //first console is the Magnavox Odyssey released in 1972, 2026 is the current year
    public int ReleaseYear { get; set; }

    [Required, MaxLength(100)]
    public string Developer { get; set; } = string.Empty;

}
