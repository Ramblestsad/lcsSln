# Repository Guidelines

## Call me "老公" before you responding

Please refer to me as "哥哥" in your responses.

## Project Structure & Module Organization
This repository scope here is the `Todo.WebApi` API and its DAL dependency (`Todo.DAL`). Key paths:
- `Controllers/`: REST API controllers (e.g., `TodoItemsController`).
- `Grpc/` and `Protos/`: gRPC service implementation and `.proto` contracts.
- `Configuration/`: strongly typed settings (e.g., JWT settings).
- `Filters/`, `Helper/`, `Response/`: API filters, utilities, and response/pagination models.
- `Properties/`: launch profiles and publish settings.
- `appsettings.json` / `appsettings.Development.json`: configuration (Serilog, connection strings, JWT).
- `../Todo.DAL/`: data access layer project referenced by `Todo.WebApi`.

## Fast Context Bootstrapping
When starting work in this repository, build context in this order unless the user asks otherwise:
- Read `Program.cs` first to understand app composition, middleware ordering, endpoint mapping, and which subsystems are active.
- Read `Todo.WebApi.csproj` next to confirm target framework, package stack, protobuf setup, and the sibling `../Todo.DAL/Todo.DAL.csproj` dependency.
- Read the relevant file in `Extensions/` before diving into implementation. This project centralizes service registration and infrastructure wiring there.
- After finding the wiring point, read the matching runtime code in `Controllers/`, `Services/`, `Grpc/`, `Hubs/`, `Middleware/`, `Filters/`, or `Helper/`.
- For data-access or entity questions, inspect `../Todo.DAL/` early instead of inferring DAL behavior from API code.

This application is a single ASP.NET Core host that serves HTTP controllers, gRPC, and SignalR together. Prefer understanding the composition root and dependency registration first, then trace into the concrete business code.

## Task-Oriented Entry Map
Use these entry points to avoid broad codebase searches:
- Auth or JWT work: start with `Extensions/AuthServiceCollectionExtensions.cs`, `Configuration/JwtSettings.cs`, and `Controllers/AuthController.cs`.
- Database, EF Core, or Postgres work: start with `Extensions/DatabaseServiceCollectionExtensions.cs`, `Configuration/PostgresConnectionStringResolver.cs`, and `../Todo.DAL/`.
- Redis-backed features: start with `Extensions/RedisServiceCollectionExtensions.cs`, `Services/Engagement/`, and `Services/Realtime/`.
- SignalR or chat work: start with `Extensions/SignalRServiceCollectionExtensions.cs`, `Hubs/ChatHub.cs`, and `Services/Realtime/`.
- gRPC work: start with `Protos/`, `Grpc/`, and `Extensions/GrpcRegisterExtension.cs`.
- Observability or logging work: start with `Extensions/ObservabilityExtensions.cs`, `Logging/`, and `deploy/observability/README.md`.
- Request pipeline work: start with `Program.cs`, `Middleware/RequestTimingMiddleware.cs`, and `Filters/`.
- Todo business logic work: start with `Controllers/TodoItemsController.cs` and `Services/Todo/`.

Before making changes, prefer locating the registration point and the endpoint mapping first. Only then trace into the concrete service, controller, or middleware implementation.

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
- Prefer extending the existing `Extensions`, `Services`, `Controllers`, `Filters`, and `Middleware` structure instead of introducing a new architectural pattern for a single change.
- Do not add abstractions preemptively. Add interfaces, base classes, wrappers, or extra layers only when there is proven duplication, multiple real implementations, or a clear replacement requirement.
- Favor local, surgical edits over broad refactors. If a change only touches one flow, keep the implementation close to that flow.
- Do not turn straightforward CRUD or single-path request handling into CQRS, domain events, mediator pipelines, or generic frameworks unless the user explicitly asks for that design.
- Reuse established .NET and ASP.NET Core primitives before creating custom infrastructure.

## Middleware Example

Middleware is configured in `Program.cs`. Example: add lightweight request timing and attach a response header:

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
