using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Serilog.Events;
using Serilog;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.BLL.Services.Instagram;
using Streetcode.BLL.Services.Payment;
using Serilog.Sinks.SystemConsole.Themes;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.BLL.Interfaces.Payment;

namespace Streetcode.WebApi.Extensions;

public static class ConfigureHostBuilderExtensions
{
    public static bool DefaultSslValidation(
        HttpRequestMessage message,
        X509Certificate2 cert,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }

        return false;
    }

    public static void ConfigureApplication(this ConfigureHostBuilder host)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

        host.ConfigureAppConfiguration((_, config) => { config.ConfigureCustom(environment); });
    }

    public static void ConfigureBlob(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<BlobEnvironmentVariables>(builder.Configuration.GetSection("Blob"));
    }

    public static void ConfigurePayment(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<PaymentEnvirovmentVariables>(builder.Configuration.GetSection("Payment"));
        services.AddHttpClient<IPaymentService, PaymentService>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = DefaultSslValidation
            };
            return handler;
        });
    }

    public static void ConfigureInstagram(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<InstagramEnvirovmentVariables>(builder.Configuration.GetSection("Instagram"));
        services.AddHttpClient<IInstagramService, InstagramService>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = DefaultSslValidation
            };
            return handler;
        });
    }

    public static void ConfigureSerilog(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(builder.Configuration);
        });
    }
}