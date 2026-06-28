# Repository Guidelines

## Call me "老公" before you responding

Please refer to me as "哥哥" in your responses.

## Project Structure & Module Organization
This repository scope here is the `src/Todo.WebApi` API and its DAL dependency (`src/Todo.DAL`). Key paths:
- `Auth/`: authentication contracts and Minimal API endpoints.
- `Todos/`: Todo endpoints, contracts, query/write modules, and pagination helpers.
- `Orders/`: order endpoint, contracts, and order write module.
- `Engagement/`: Redis-backed engagement endpoints, contracts, and Redis adapter.
- `Chat/`: chat hub, client contract, DTOs, and room state adapter.
- `Greeter/`: gRPC service implementation and `.proto` contract.
- `Infrastructure/`: configuration, service registration, logging, middleware, mapping, request context, and validation helpers.
- `../Todo.DAL/`: data access project, also organized by feature (`Todos`, `Orders`, `Inventory`, `Messaging`, `Data`).

## Fast Context Bootstrapping
When starting work in this project, build context in this order unless the user asks otherwise:
- Read `Program.cs` first to understand middleware ordering and endpoint mapping.
- Read `Todo.WebApi.csproj` next to confirm target framework, package stack, protobuf setup, and the sibling `../Todo.DAL/Todo.DAL.csproj` dependency.
- Read the relevant file in `Infrastructure/Extensions/` before diving into implementation.
- After finding the wiring point, read the matching runtime code in the feature folder (`Auth/`, `Todos/`, `Orders/`, `Engagement/`, `Chat/`, or `Greeter/`).
- For data-access or entity questions, inspect `../Todo.DAL/` early instead of inferring DAL behavior from API code.

This application is a single ASP.NET Core host that serves Minimal API HTTP endpoints, gRPC, and SignalR together.

## Task-Oriented Entry Map
Use these entry points to avoid broad searches:
- Auth or JWT work: start with `Infrastructure/Extensions/AuthServiceCollectionExtensions.cs`, `Infrastructure/Configuration/JwtSettings.cs`, and `Auth/AuthEndpoints.cs`.
- Database, EF Core, or Postgres work: start with `Infrastructure/Extensions/DatabaseServiceCollectionExtensions.cs`, `Infrastructure/Configuration/PostgresConnectionStringResolver.cs`, and `../Todo.DAL/`.
- Redis-backed features: start with `Infrastructure/Extensions/RedisServiceCollectionExtensions.cs`, `Engagement/`, and `Chat/`.
- Chat work: start with `Infrastructure/Extensions/SignalRServiceCollectionExtensions.cs` and `Chat/ChatHub.cs`.
- gRPC work: start with `Greeter/`, `Greeter/Protos/`, and `Infrastructure/Extensions/GrpcRegisterExtension.cs`.
- Observability or logging work: start with `Infrastructure/Extensions/ObservabilityExtensions.cs`, `Infrastructure/Logging/`, and `deploy/observability/README.md`.
- Request pipeline work: start with `Program.cs` and `Infrastructure/Middleware/RequestTimingMiddleware.cs`.
- Todo business logic work: start with `Todos/TodoEndpoints.cs`, `Todos/TodoQueryService.cs`, and `Todos/TodoCommandService.cs`.

## Build, Test, and Development Commands
From the solution root:
- `dotnet build lcsSln.sln`: compile the solution.
- `dotnet run --project src/Todo.WebApi/Todo.WebApi.csproj`: run the API using the default profile.
- `dotnet run --project src/Todo.WebApi/Todo.WebApi.csproj --launch-profile Local`: run with the `Local` profile.
- `dotnet test tests/Todo.WebApi.Tests/Todo.WebApi.Tests.csproj`: run API tests.

The app listens on ports `8080` (HTTP/1.1 + HTTP/2) and `8081` (HTTP/2) as configured in `Program.cs`.

## Coding Style & Naming Conventions
- Language: C# with nullable reference types enabled.
- Indentation: 4 spaces, braces on new lines.
- Naming: PascalCase for types/methods/properties, camelCase for locals and parameters.
- Keep endpoint handlers concise. Put non-trivial business work in the feature module behind the endpoint.
- Prefer extending existing feature folders and `Infrastructure/Extensions` over adding a new pattern.
- Do not add interfaces or extra layers unless there is proven duplication, multiple real implementations, or a clear replacement need.
- Favor local edits over broad refactors.
- Reuse ASP.NET Core and .NET primitives before creating custom infrastructure.

## Testing Guidelines
The API test project lives at `tests/Todo.WebApi.Tests/`.
- Naming: `*Tests.cs` per subject.
- Run API tests with `dotnet test tests/Todo.WebApi.Tests/Todo.WebApi.Tests.csproj` from the solution root.

## Commit & Pull Request Guidelines
Recent commits use Conventional Commits (e.g., `feat: ...`, `fix: ...`). Follow that style.
PRs should include:
- A concise description of changes and motivation.
- Linked issues when applicable.
- API behavior notes or sample requests when endpoints change.

## Security & Configuration Tips
Do not commit real secrets. Use `appsettings.Development.json` or environment variables for local overrides. Pay attention to `ConnectionStrings:postgresWrite`, `ConnectionStrings:postgresRead`, `Redis`, and `Jwt` settings when running locally.
