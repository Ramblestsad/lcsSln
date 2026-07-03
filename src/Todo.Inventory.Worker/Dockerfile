# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
WORKDIR /src
COPY ["src/Todo.Inventory.Worker/Todo.Inventory.Worker.csproj", "src/Todo.Inventory.Worker/"]
COPY ["src/Todo.DAL/Todo.DAL.csproj", "src/Todo.DAL/"]
RUN dotnet restore "src/Todo.Inventory.Worker/Todo.Inventory.Worker.csproj"
COPY . .
WORKDIR /src/src/Todo.Inventory.Worker
RUN dotnet publish "Todo.Inventory.Worker.csproj" -c Release -o /app/publish\
    -p:UseAppHost=false\
    -p:DebugType=None\
    -p:DebugSymbols=false

FROM mcr.microsoft.com/dotnet/runtime:10.0-noble AS final
WORKDIR /app
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Inventory.Worker.dll"]
