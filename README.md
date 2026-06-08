## Project Structure

```
GameCatalogue/
├── GameCatalogue.API/          # ASP.NET Core 8 Web API
│   ├── Controllers/            # GamesController (CRUD)
│   ├── Data/                   # GameDbContext (EF Core Code-First)
│   ├── Migrations/             # Auto-generated EF migrations
│   ├── Models/                 # Game entity
│   ├── Repositories/           # IGameRepository + GameRepository
│   └── Program.cs              # DI setup, CORS, EF migration on startup
│
├── GameCatalogue.Tests/        # xUnit unit tests (Moq)
│   └── GamesControllerTests.cs # 9 tests covering all CRUD paths
│
└── game-catalogue-ui/          # Angular 22 SPA
    └── src/app/
        ├── models/             # game.model.ts
        ├── services/           # game.service.ts (HttpClient wrapper)
        └── pages/
            ├── browse/         # Game list table with delete modal
            └── edit/           # Create / edit form with validation
```

---

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 8.x |
| SQL Server | LocalDB (included with VS) or full instance |
| Node.js | 18+ |

---

## Running the API

```bash
# From repo root — runs migrations + seeds 5 games automatically
cd GameCatalogue.API
dotnet run
# API listens on http://localhost:5183
```

### Connection String
Edit `GameCatalogue.API/appsettings.json` if you need a different SQL Server instance:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GameCatalogue1;Trusted_Connection=True;"
```

---

## Running Tests

```bash
# From repo root
dotnet test
# Expected: 9 passed, 0 failed
```
