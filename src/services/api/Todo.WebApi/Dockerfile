# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /src
COPY ["src/services/api/Todo.WebApi/Todo.WebApi.csproj", "src/services/api/Todo.WebApi/"]
COPY ["src/services/api/Todo.DAL/Todo.DAL.csproj", "src/services/api/Todo.DAL/"]
COPY ["src/shared/Todo.Observability/Todo.Observability.csproj", "src/shared/Todo.Observability/"]
RUN dotnet restore "src/services/api/Todo.WebApi/Todo.WebApi.csproj"
COPY . .

WORKDIR /src/src/services/api/Todo.WebApi
# RUN dotnet build -c Release -o /app/build
RUN dotnet publish "Todo.WebApi.csproj" -c Release -o /app/publish\
    -p:UseAppHost=false\
    -p:DebugType=None\
    -p:DebugSymbols=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.WebApi.dll"]
