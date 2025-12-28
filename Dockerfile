# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/runtime-deps:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Todo.WebApi/Todo.WebApi.csproj", "Todo.WebApi/"]
COPY ["Todo.DAL/Todo.DAL.csproj", "Todo.DAL/"]
RUN dotnet restore "Todo.WebApi/Todo.WebApi.csproj"
COPY . .

WORKDIR "/src/Todo.WebApi"
#dotnet publish Todo.WebApi/Todo.WebApi.csproj -c Release \
#  -r osx-arm64 --self-contained true /p:PublishSingleFile=true
RUN dotnet publish "Todo.WebApi.csproj" -c Release -o /app/publish -p:PublishProfile=Docker-linux-arm64

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./Todo.WebApi"]
