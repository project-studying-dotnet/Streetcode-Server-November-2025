using Hangfire;
using DotNetEnv;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.WebApi.Extensions;
using Streetcode.WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../../.env");

var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbPassword = Environment.GetEnvironmentVariable("DB_USER_PASSWORD");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

var connectionString =
   $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";

builder.Configuration.AddEnvironmentVariables();
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

builder.Host.ConfigureApplication();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddCustomServices();
builder.Services.ConfigureBlob(builder);
builder.Services.ConfigurePayment(builder);
builder.Services.ConfigureInstagram(builder);
builder.Services.ConfigureSerilog(builder);
var app = builder.Build();

if (app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
}
else
{
    app.UseHsts();
}

await app.ApplyMigrations();

// await app.SeedDataAsync(); // uncomment for seeding data in local
app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dash");

if (app.Environment.EnvironmentName != "Local")
{
    BackgroundJob.Schedule<WebParsingUtils>(
    wp => wp.ParseZipFileFromWebAsync(), TimeSpan.FromMinutes(1));
    RecurringJob.AddOrUpdate<WebParsingUtils>(
        "parse-zip-file-monthly",
        wp => wp.ParseZipFileFromWebAsync(),
        Cron.Monthly);
    RecurringJob.AddOrUpdate<BlobService>(
        "clean-blob-storage-monthly",
        b => b.CleanBlobStorage(),
        Cron.Monthly);
}

app.MapControllers();

app.Run();
public partial class Program
{
}