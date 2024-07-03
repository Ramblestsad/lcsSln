FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER root
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Scratch/Scratch.csproj", "Scratch/"]
RUN dotnet restore "Scratch/Scratch.csproj"
COPY . .
WORKDIR "/src/Scratch"
RUN dotnet build "Scratch.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Scratch.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scratch.dll"]
