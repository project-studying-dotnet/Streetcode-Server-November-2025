# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Streetcode.sln .
COPY Services/Streetcode.ApiGateway/Streetcode.ApiGateway/ Services/Streetcode.ApiGateway/Streetcode.ApiGateway/

WORKDIR /src/Services/Streetcode.ApiGateway/Streetcode.ApiGateway
RUN dotnet restore "Streetcode.ApiGateway.csproj"

COPY Services/Streetcode.ApiGateway/Streetcode.ApiGateway/ .

RUN dotnet build "Streetcode.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/Streetcode.ApiGateway/Streetcode.ApiGateway
RUN dotnet publish "Streetcode.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN groupadd -r nonuser \
    && useradd -r -g nonuser -s /sbin/nologin appuser \
    && chown -R appuser:nonuser /app

ENV ASPNETCORE_ENVIRONMENT=Docker

USER appuser

ENTRYPOINT ["dotnet", "Streetcode.ApiGateway.dll"]