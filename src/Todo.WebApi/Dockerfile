# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /src
COPY ["src/Todo.WebApi/Todo.WebApi.csproj", "src/Todo.WebApi/"]
COPY ["src/Todo.DAL/Todo.DAL.csproj", "src/Todo.DAL/"]
COPY ["src/Todo.Observability/Todo.Observability.csproj", "src/Todo.Observability/"]
RUN dotnet restore "src/Todo.WebApi/Todo.WebApi.csproj"
COPY . .

WORKDIR /src/src/Todo.WebApi
# RUN dotnet build -c Release -o /app/build
RUN dotnet publish "Todo.WebApi.csproj" -c Release -o /app/publish\
    -p:UseAppHost=false\
    -p:DebugType=None\
    -p:DebugSymbols=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo.WebApi.dll"]
