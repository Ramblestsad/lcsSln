# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-amd64 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble-amd64 AS build
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /src
COPY ["Todo.WebApi/Todo.WebApi.csproj", "Todo.WebApi/"]
COPY ["Todo.DAL/Todo.DAL.csproj", "Todo.DAL/"]
RUN dotnet restore "Todo.WebApi/Todo.WebApi.csproj"
COPY . .

WORKDIR /src/Todo.WebApi
RUN dotnet build -c Release -o /app/build
RUN dotnet publish "Todo.WebApi.csproj" -c Release -o /app/publish \
    -p:PublishProfile=docker \
    -p:SelfContained=false \
    -p:PublishSingleFile=true \
    -p:DebugType=None \
    -p:DebugSymbols=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./Todo.WebApi"]
