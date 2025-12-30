using System;
using System.IO;
using DotNetEnv;

namespace Streetcode.WebApi.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder ConfigureCustom(this IConfigurationBuilder builder, string environment)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables("STREETCODE_");

            return builder;
        }

        public static void LoadEnvironmentVariables(this ConfigurationManager configuration)
        {
            TryLoadDotEnv();

            var dbServer = Environment.GetEnvironmentVariable("DB_SERVER")
                        ?? Environment.GetEnvironmentVariable("DOCKER_DB_SERVER")
                        ?? Environment.GetEnvironmentVariable("DATABASE_HOST");

            var dbPassword = Environment.GetEnvironmentVariable("DB_USER_PASSWORD")
                         ?? Environment.GetEnvironmentVariable("DOCKER_DB_PASSWD")
                         ?? Environment.GetEnvironmentVariable("DOCKER_DB_PASSWORD")
                         ?? Environment.GetEnvironmentVariable("DB_PASSWORD");

            var dbUser = Environment.GetEnvironmentVariable("DB_USER")
                      ?? Environment.GetEnvironmentVariable("DOCKER_USER_VALUE")
                      ?? Environment.GetEnvironmentVariable("DATABASE_USER");

            var dbName = Environment.GetEnvironmentVariable("DB_NAME")
                     ?? Environment.GetEnvironmentVariable("DOCKER_DB_NAME")
                     ?? Environment.GetEnvironmentVariable("DATABASE_NAME")
                     ?? "StreetcodeDb";

            if (!string.IsNullOrWhiteSpace(dbServer) && !string.IsNullOrWhiteSpace(dbUser))
            {
                var connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";
                configuration["ConnectionStrings:DefaultConnection"] = connectionString;
            }

            configuration["JwtSettings:SecretKey"] = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                                                 ?? Environment.GetEnvironmentVariable("JWT_SECRET");
            configuration["JwtSettings:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
            configuration["JwtSettings:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            configuration["JwtSettings:AccessTokenExpirationMinutes"] = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRATION_MINUTES") ?? "15";
            configuration["JwtSettings:RefreshTokenExpirationMinutes"] = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRATION_MINUTES") ?? "10080";
            configuration["JwtSettings:RequireHttpsMetadata"] = Environment.GetEnvironmentVariable("JWT_REQUIRE_HTTPS_METADATA") ?? "true";
            configuration["JwtSettings:RefreshTokenAutoDeleteTime"] = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_AUTO_DELETE_TIME") ?? "1440";
        }

        private static void TryLoadDotEnv(int maxUpSearch = 6)
        {
            try
            {
                var startPaths = new[]
                {
                    Directory.GetCurrentDirectory(),
                    AppContext.BaseDirectory
                };

                foreach (var start in startPaths)
                {
                    if (string.IsNullOrEmpty(start))
                    {
                        continue;
                    }

                    var dir = new DirectoryInfo(start);
                    for (var i = 0; i < maxUpSearch && dir != null; i++)
                    {
                        var candidate = Path.Combine(dir.FullName, ".env");
                        if (File.Exists(candidate))
                        {
                            Env.Load(candidate);
                            return;
                        }

                        dir = dir.Parent;
                    }
                }

                var relativePathForEnvFile = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".env"));
                if (File.Exists(relativePathForEnvFile))
                {
                    Env.Load(relativePathForEnvFile);
                    return;
                }
            }
            catch
            {
                throw new ArgumentNullException("Failed to load .env file");
            }
        }
    }
}