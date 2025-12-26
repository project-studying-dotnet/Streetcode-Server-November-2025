# -----------------------------
# Base runtime image (Windows)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-ltsc2022 AS base
WORKDIR C:\app
EXPOSE 8080
EXPOSE 8081

# -----------------------------
# Build stage (Windows SDK)
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR C:\src

# Copy csproj files first for caching
COPY Services\Streetcode.Auth\Streetcode.Auth.Api\Streetcode.Auth.Api.csproj Services\Streetcode.Auth\Streetcode.Auth.Api\
COPY Services\Streetcode.Auth\Streetcode.Auth.Application\Streetcode.Auth.Application.csproj Services\Streetcode.Auth\Streetcode.Auth.Application\
COPY Services\Streetcode.Auth\Streetcode.Auth.Common\Streetcode.Auth.Common.csproj Services\Streetcode.Auth\Streetcode.Auth.Common\
COPY Services\Streetcode.Auth\Streetcode.Auth.Domain\Streetcode.Auth.Domain.csproj Services\Streetcode.Auth\Streetcode.Auth.Domain\
COPY Services\Streetcode.Auth\Streetcode.Auth.Infrastructure\Streetcode.Auth.Infrastructure.csproj Services\Streetcode.Auth\Streetcode.Auth.Infrastructure\
COPY Services\BuildingBlocks\Streetcode.BuildingBlocks\Streetcode.BuildingBlocks.csproj Services\BuildingBlocks\Streetcode.BuildingBlocks\
COPY Services\BuildingBlocks\Streetcode.Messaging\Streetcode.Messaging.csproj Services\BuildingBlocks\Streetcode.Messaging\

RUN dotnet restore Services\Streetcode.Auth\Streetcode.Auth.Api\Streetcode.Auth.Api.csproj

# Copy source
COPY Services\Streetcode.Auth\ Services\Streetcode.Auth\
COPY Services\BuildingBlocks\ Services\BuildingBlocks\

WORKDIR C:\src\Services\Streetcode.Auth\Streetcode.Auth.Api
RUN dotnet build Streetcode.Auth.Api.csproj -c %BUILD_CONFIGURATION% -o C:\app\build

# -----------------------------
# Publish stage
# -----------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Streetcode.Auth.Api.csproj -c %BUILD_CONFIGURATION% -o C:\app\publish /p:UseAppHost=false

# -----------------------------
# Final runtime image
# -----------------------------
FROM base AS final
WORKDIR C:\app
COPY --from=publish C:\app\publish .

ENTRYPOINT ["dotnet", "Streetcode.Auth.Api.dll"]
