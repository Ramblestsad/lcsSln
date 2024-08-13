# syntax=docker/dockerfile:1

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# copy everything else and build app
WORKDIR /source/Todo.WebApi
RUN dotnet publish Todo.WebApi -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./Todo.WebApi"]
