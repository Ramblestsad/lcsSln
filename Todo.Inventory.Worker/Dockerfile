# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
WORKDIR /src
COPY ["Todo.Inventory.Worker/Todo.Inventory.Worker.csproj", "Todo.Inventory.Worker/"]
COPY ["Todo.DAL/Todo.DAL.csproj", "Todo.DAL/"]
RUN dotnet restore "Todo.Inventory.Worker/Todo.Inventory.Worker.csproj"
COPY . .
WORKDIR /src/Todo.Inventory.Worker
RUN dotnet publish "Todo.Inventory.Worker.csproj" -c Release -o /app/publish \
    -p:SelfContained=false \
    -p:DebugType=None \
    -p:DebugSymbols=false

FROM mcr.microsoft.com/dotnet/runtime:10.0-noble AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Inventory.Worker.dll"]
