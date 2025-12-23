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
            // Database configuration
            var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_USER_PASSWORD");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";
            configuration["ConnectionStrings:DefaultConnection"] = connectionString;

            // JWT configuration
            configuration["JwtSettings:SecretKey"] = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            configuration["JwtSettings:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
            configuration["JwtSettings:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            configuration["JwtSettings:AccessTokenExpirationMinutes"] = Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRATION_MINUTES") ?? "15";
            configuration["JwtSettings:RefreshTokenExpirationMinutes"] = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_EXPIRATION_MINUTES") ?? "10080";
            configuration["JwtSettings:RequireHttpsMetadata"] = Environment.GetEnvironmentVariable("JWT_REQUIRE_HTTPS_METADATA") ?? "true";
            configuration["JwtSettings:RefreshTokenAutoDeleteTime"] = Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_AUTO_DELETE_TIME") ?? "1440";
        }
    }
}