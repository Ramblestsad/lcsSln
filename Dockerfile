# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 62435
EXPOSE 62436

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR /src/Todo.WebApi
RUN dotnet publish -c Release -o /src/publish

FROM base AS final
WORKDIR /app
COPY --from=build /src/publish .

ENTRYPOINT ["./Todo.WebApi"]
