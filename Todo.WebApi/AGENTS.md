# Repository Guidelines

## Call me "老公" before you responding

Please refer to me as "老公" in your responses.

## Project Structure & Module Organization
This repository scope here is the `Todo.WebApi` API and its DAL dependency (`Todo.DAL`). Key paths:
- `Controllers/`: REST API controllers (e.g., `TodoItemsController`).
- `Grpc/` and `Protos/`: gRPC service implementation and `.proto` contracts.
- `Configuration/`: strongly typed settings (e.g., JWT settings).
- `Filters/`, `Helper/`, `Response/`: API filters, utilities, and response/pagination models.
- `Properties/`: launch profiles and publish settings.
- `appsettings.json` / `appsettings.Development.json`: configuration (Serilog, connection strings, JWT).
- `../Todo.DAL/`: data access layer project referenced by `Todo.WebApi`.

## Build, Test, and Development Commands
- `dotnet build`: compile the web API project.
- `dotnet run --project Todo.WebApi.csproj`: run the API using the default profile.
- `dotnet run --project Todo.WebApi.csproj --launch-profile Local`: run with the `Local` profile from `Properties/launchSettings.json`.
- `dotnet test`: run tests (add a test project if needed; none exists here yet).

The app listens on ports `8080` (HTTP/1.1 + HTTP/2) and `8081` (HTTP/2) as configured in `Program.cs`.

## Coding Style & Naming Conventions
- Language: C# with nullable reference types enabled.
- Indentation: 4 spaces, braces on new lines (standard C# style).
- Naming: PascalCase for types/methods/properties, camelCase for locals and parameters.
- Keep controller actions concise and use async patterns for I/O (see `Controllers/`).

## Middleware Example

Middleware is configured in `Startup.Configure`. Example: add lightweight request timing and attach a response header:

```csharp
app.Use(async (context, next) =>
{
    var started = DateTimeOffset.UtcNow;
    await next();
    var elapsedMs = (DateTimeOffset.UtcNow - started).TotalMilliseconds;
    context.Response.Headers["X-Request-Duration-Ms"] = elapsedMs.ToString("F0");
});
```

## Testing Guidelines
There is no dedicated test project in this repository. If you add tests, prefer:
- A sibling test project (e.g., `Todo.WebApi.Tests/`) using xUnit or NUnit.
- Naming: `*Tests.cs` per subject (e.g., `TodoItemsControllerTests.cs`).
- Run with `dotnet test` from the repo root.

## Commit & Pull Request Guidelines
Recent commits use Conventional Commits (e.g., `feat: ...`, `fix: ...`). Follow that style for clarity.
PRs should include:
- A concise description of changes and motivation.
- Linked issues (if applicable).
- API behavior notes or sample requests when endpoints change.
- Screenshots only if UI or documentation output changes.

## Security & Configuration Tips
Do not commit real secrets. Use `appsettings.Development.json` or environment variables for local overrides. Pay attention to `ConnectionStrings:postgres` and `Jwt` settings when running locally.
