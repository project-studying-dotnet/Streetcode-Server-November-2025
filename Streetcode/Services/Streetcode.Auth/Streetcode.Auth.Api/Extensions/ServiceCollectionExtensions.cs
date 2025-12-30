using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.Auth.Application.Mapping.Users;
using Streetcode.Auth.Application.Repositories.Interfaces.ResfreshTokens;
using Streetcode.Auth.Common.Configurations;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.Auth.Infrastructure.Data;
using Streetcode.Auth.Infrastructure.Repositories.Realizations.RefreshTokens;
using Streetcode.Auth.Infrastructure.Services.Token;
using Streetcode.BuildingBlocks.Interfaces.Logging;
using Streetcode.BuildingBlocks.Services.Logging;
using Streetcode.Messaging.Extensions;

namespace Streetcode.Auth.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            var corsConfig = configuration
                                 .GetSection("CORS")
                                 .Get<CorsConfigurations>()
                             ?? throw new InvalidOperationException("CORS configuration is missing");

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(corsConfig.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddHsts(opt =>
            {
                opt.Preload = true;
                opt.IncludeSubDomains = true;
                opt.MaxAge = TimeSpan.FromDays(30);
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddScoped<ITokenService, TokenService>();


            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(UserProfile).Assembly));

            services.AddAutoMapper(typeof(UserProfile).Assembly);

            return services;
        }

        public static IServiceCollection ConfigureSerilog(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(builder.Configuration);
            });

            return services;
        }

        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["MessageBroker:Provider"];
            
            switch (provider)
            {
                case "RabbitMQ":
                    services.AddRabbitMqMessageBroker(configuration, Assembly.GetExecutingAssembly());
                    return services;
                case "AzureServiceBus":
                    services.AddAzureServiceBusMessageBroker(configuration, Assembly.GetExecutingAssembly());
                    return services;
                default:
                    throw new InvalidOperationException($"Unsupported Message Broker Provider: {provider}");
            }
        }

        public static IServiceCollection AddOtlp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                    resource.AddService(
                        serviceName: configuration["OTEL_SERVICE_NAME"] ?? throw new InvalidOperationException(
                            "OTEL_SERVICE_NAME configuration value is required."),
                        serviceVersion: "1.0.0"))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation(options =>
                            options.SetDbStatementForText = true)
                        .AddSource("MassTransit")
                        .AddOtlpExporter();
                });

            services.AddLogging(logging =>
            {
                logging.AddOpenTelemetry(options =>
                {
                    options.AddOtlpExporter();
                });
            });

            return services;
        }
    }
}
