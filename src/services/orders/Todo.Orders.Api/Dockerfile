# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
WORKDIR /src
COPY ["src/services/orders/Todo.Orders.Api/Todo.Orders.Api.csproj", "src/services/orders/Todo.Orders.Api/"]
COPY ["src/services/orders/Todo.Orders/Todo.Orders.csproj", "src/services/orders/Todo.Orders/"]
COPY ["src/shared/Todo.Contracts/Todo.Contracts.csproj", "src/shared/Todo.Contracts/"]
COPY ["src/shared/Todo.Observability/Todo.Observability.csproj", "src/shared/Todo.Observability/"]
RUN dotnet restore "src/services/orders/Todo.Orders.Api/Todo.Orders.Api.csproj"
COPY . .
WORKDIR /src/src/services/orders/Todo.Orders.Api
RUN dotnet publish "Todo.Orders.Api.csproj" -c Release -o /app/publish \
    -p:UseAppHost=false -p:DebugType=None -p:DebugSymbols=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Orders.Api.dll"]
