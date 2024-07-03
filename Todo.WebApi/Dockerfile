FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Todo.WebApi/Todo.WebApi.csproj", "Todo.WebApi/"]
COPY ["Todo.DAL/Todo.DAL.csproj", "Todo.DAL/"]
RUN dotnet restore "Todo.WebApi/Todo.WebApi.csproj"
COPY . .
WORKDIR "/src/Todo.WebApi"
RUN dotnet build "Todo.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todo.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.WebApi.dll"]
