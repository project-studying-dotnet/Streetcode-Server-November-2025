using System.Reflection;
using System.Text;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Events;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Cache;
using Streetcode.BLL.Interfaces.Email;
using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.BLL.Interfaces.Jwt;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Interfaces.Payment;
using Streetcode.BLL.Interfaces.Text;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.BLL.Services.Cache;
using Streetcode.BLL.Services.Email;
using Streetcode.BLL.Services.Instagram;
using Streetcode.BLL.Services.Jwt;
using Streetcode.BLL.Services.Logging;
using Streetcode.BLL.Services.Payment;
using Streetcode.BLL.Services.Text;
using Streetcode.DAL.Entities.AdditionalContent.Email;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddRepositoryServices();
        services.AddFeatureManagement();
        services.AddMemoryCache();
        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddAutoMapper(currentAssemblies);
        services.AddMediatR(currentAssemblies);

        // Register FluentValidation
        services.AddValidatorsFromAssembly(Assembly.Load("Streetcode.BLL"));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BLL.MediatR.ValidationBehavior<,>));

        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IInstagramService, InstagramService>();
        services.AddScoped<ITextService, AddTermsToTextService>();

        services.AddCachingServices();
    }

    public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        services.AddSingleton(emailConfig);

        services.AddDbContext<StreetcodeDbContext>(options =>
        {
            options.UseSqlServer(connectionString, opt =>
            {
                opt.MigrationsAssembly(typeof(StreetcodeDbContext).Assembly.GetName().Name);
                opt.MigrationsHistoryTable("__EFMigrationsHistory", schema: "entity_framework");
            });
        });

        // ASP.NET Identity setup for UserManager / RoleManager
        services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<StreetcodeDbContext>()
                .AddUserManager<UserManager<User>>();

        // JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = configuration.GetValue<bool>("JwtSettings:RequireHttpsMetadata", true);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        // Register JwtService
        services.AddScoped<IJwtService>(provider =>
        {
            var repository = provider.GetRequiredService<IRepositoryWrapper>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            return new JwtService(
                secretKey: secretKey,
                issuer: issuer,
                audience: audience,
                repository: repository,
                userManager: userManager,
                accessTokenExpirationMinutes: int.TryParse(jwtSettings["AccessTokenExpirationMinutes"], out var accessExpiration) ? accessExpiration : 15,
                refreshTokenExpirationMinutes: int.TryParse(jwtSettings["RefreshTokenExpirationMinutes"], out var refreshExpiration) ? refreshExpiration : 600);
        });

        services.AddBlobStorageServices(configuration);

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(connectionString);
        });

        services.AddHangfireServer();

        var corsConfig = configuration.GetSection("CORS").Get<CorsConfiguration>();
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
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

        services.AddLogging();
        services.AddControllers();
    }

    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
            opt.CustomSchemaIds(x => x.FullName);
        });
    }

    public static IServiceCollection AddCachingServices(this IServiceCollection services, ILogger? logger = null)
    {
        var redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");

        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            Console.WriteLine($"[CACHE] Redis connection string found: {redisConnectionString}");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "Streetcode_";
            });

            services.AddSingleton<ICacheService, RedisCacheService>();
            Console.WriteLine("[CACHE] Using RedisCacheService");
        }
        else
        {
            services.AddSingleton<ICacheService, NoCacheService>();
        }

        return services;
    }

    public static IServiceCollection AddBlobStorageServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<BlobEnvironmentVariables>(
        configuration.GetSection("Blob"));

        var blobConfig = configuration
            .GetSection("Blob")
            .Get<BlobEnvironmentVariables>()
            ?? new BlobEnvironmentVariables();

        if (blobConfig.BlobStorageType == BlobStorageType.Azure)
        {
            Console.WriteLine("[BLOB] Registering AzureBlobService");

            services.AddScoped<IBlobService, AzureBlobService>();
        }
        else
        {
            Console.WriteLine("[BLOB] Registering LocalBlobService");
            Directory.CreateDirectory(blobConfig.BlobStorePath);

            services.AddScoped<IBlobService, LocalBlobService>();
        }

        return services;
    }

    public class CorsConfiguration
    {
        public List<string> AllowedOrigins { get; set; }
        public List<string> AllowedHeaders { get; set; }
        public List<string> AllowedMethods { get; set; }
        public int PreflightMaxAge { get; set; }
    }
}
