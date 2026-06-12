# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
WORKDIR /src
COPY ["src/Todo.Order.Worker/Todo.Order.Worker.csproj", "src/Todo.Order.Worker/"]
COPY ["src/Todo.DAL/Todo.DAL.csproj", "src/Todo.DAL/"]
RUN dotnet restore "src/Todo.Order.Worker/Todo.Order.Worker.csproj"
COPY . .
WORKDIR /src/src/Todo.Order.Worker
RUN dotnet publish "Todo.Order.Worker.csproj" -c Release -o /app/publish \
    -p:SelfContained=false \
    -p:DebugType=None \
    -p:DebugSymbols=false

FROM mcr.microsoft.com/dotnet/runtime:10.0-noble AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.Order.Worker.dll"]
