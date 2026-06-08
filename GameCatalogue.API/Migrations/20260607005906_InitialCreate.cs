using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameCatalogue.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Developer", "Genre", "Platform", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "Nintendo", "Action-Adventure", "Nintendo Switch", 2017, "The Legend of Zelda: Breath of the Wild" },
                    { 2, "FromSoftware", "Action RPG", "PC / PS5 / Xbox Series X", 2022, "Elden Ring" },
                    { 3, "Rockstar Games", "Action-Adventure", "PC / PS4 / Xbox One", 2018, "Red Dead Redemption 2" },
                    { 4, "Supergiant Games", "Roguelike", "PC / Nintendo Switch", 2020, "Hades" },
                    { 5, "CD Projekt Red", "Action RPG", "PC / PS5 / Xbox Series X", 2020, "Cyberpunk 2077" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
