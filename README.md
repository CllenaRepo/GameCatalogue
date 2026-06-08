# Game Catalogue

A simple full-stack video game catalogue built to demonstrate **ASP.NET Core** and **Angular**.

---

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
| Angular CLI | `npm install -g @angular/cli` |

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
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GameCatalogue;Trusted_Connection=True;"
```

---

## Running the Frontend

```bash
cd game-catalogue-ui
npm install
ng serve
# App opens at http://localhost:4200
```

---

## Running Tests

```bash
# From repo root
dotnet test
# Expected: 9 passed, 0 failed
```

---

## Key Concepts Demonstrated

### C# / ASP.NET Core
- **Controller** — `[ApiController]` with attribute routing (`[HttpGet]`, `[HttpPost]`, etc.)
- **Dependency Injection** — `IGameRepository` injected via constructor
- **Repository Pattern** — decouples the controller from EF Core, makes unit testing easy
- **EF Core Code-First** — `GameDbContext` defines schema; `dotnet ef migrations add` generates SQL
- **Data Annotations** — `[Required]`, `[MaxLength]`, `[Range]` on the `Game` model
- **Seed Data** — `OnModelCreating` → `HasData(...)` populates the DB on first run

### Angular
- **Standalone Components** — no `NgModule`; imports declared directly per component
- **Signal-based state** — `signal<Game[]>([])`, `loading = signal(true)` (Angular 17+ reactivity)
- **Angular Router** — `app.routes.ts` maps `/games`, `/games/new`, `/games/edit/:id`
- **Template-driven Forms** — `FormsModule`, `ngModel`, `ngForm` for the edit page
- **`@if` / `@for` blocks** — Angular 17+ control flow syntax
- **HttpClient** — `GameService` wraps all API calls with Observables
- **ng-bootstrap** — `NgbModal` for delete confirmation, `NgbTooltipModule` for button tooltips
