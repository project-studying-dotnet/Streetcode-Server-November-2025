namespace Streetcode.XIntegrationTest.Base
{
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Streetcode.DAL.Persistence;

    /// <summary>
    /// Base class for integration tests providing a configured WebApplicationFactory
    /// with an in-memory database and test server setup.
    /// </summary>
    /// <typeparam name="TStartup">The startup class of the web application.</typeparam>
    public class BaseIntegrationTest<TStartup> : IDisposable
        where TStartup : class
    {
        protected readonly HttpClient Client;
        protected readonly WebApplicationFactory<TStartup> Factory;
        protected readonly JsonSerializerOptions JsonOptions;

        public BaseIntegrationTest()
        {
            this.Factory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("IntegrationTests");
                    
                    builder.ConfigureTestServices(services =>
                    {
                        // Remove the existing DbContext registration
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<StreetcodeDbContext>));
                        
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        // Add in-memory database for testing
                        services.AddDbContext<StreetcodeDbContext>(options =>
                        {
                            options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}");
                            options.EnableSensitiveDataLogging();
                        });

                        // Ensure the database is created
                        var serviceProvider = services.BuildServiceProvider();
                        using var scope = serviceProvider.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<StreetcodeDbContext>();
                        db.Database.EnsureCreated();
                    });
                });

            this.Client = this.Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            this.JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() },
            };
        }

        /// <summary>
        /// Seeds the database with test data using the provided action.
        /// </summary>
        /// <param name="seedAction">Action to seed the database.</param>
        protected void SeedDatabase(Action<StreetcodeDbContext> seedAction)
        {
            using var scope = this.Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StreetcodeDbContext>();
            
            seedAction(context);
            context.SaveChanges();
        }

        /// <summary>
        /// Executes an action with the database context.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        protected void ExecuteWithContext(Action<StreetcodeDbContext> action)
        {
            using var scope = this.Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StreetcodeDbContext>();
            action(context);
        }

        /// <summary>
        /// Executes a function with the database context and returns a result.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="func">Function to execute.</param>
        /// <returns>The result of the function.</returns>
        protected T ExecuteWithContext<T>(Func<StreetcodeDbContext, T> func)
        {
            using var scope = this.Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StreetcodeDbContext>();
            return func(context);
        }

        /// <summary>
        /// Sends a GET request and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <returns>The deserialized response.</returns>
        protected async Task<T?> GetAsync<T>(string url)
        {
            var response = await this.Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<T>(content, this.JsonOptions);
        }

        /// <summary>
        /// Sends a POST request with JSON content and deserializes the response.
        /// </summary>
        /// <typeparam name="TRequest">The request body type.</typeparam>
        /// <typeparam name="TResponse">The expected response type.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="data">The request data.</param>
        /// <returns>The HTTP response and deserialized content.</returns>
        protected async Task<(HttpResponseMessage Response, TResponse? Data)> PostAsync<TRequest, TResponse>(
            string url,
            TRequest data)
        {
            var json = JsonSerializer.Serialize(data, this.JsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await this.Client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var deserializedData = string.IsNullOrEmpty(responseContent)
                ? default
                : JsonSerializer.Deserialize<TResponse>(responseContent, this.JsonOptions);
            
            return (response, deserializedData);
        }

        /// <summary>
        /// Sends a PUT request with JSON content and deserializes the response.
        /// </summary>
        /// <typeparam name="TRequest">The request body type.</typeparam>
        /// <typeparam name="TResponse">The expected response type.</typeparam>
        /// <param name="url">The request URL.</param>
        /// <param name="data">The request data.</param>
        /// <returns>The HTTP response and deserialized content.</returns>
        protected async Task<(HttpResponseMessage Response, TResponse? Data)> PutAsync<TRequest, TResponse>(
            string url,
            TRequest data)
        {
            var json = JsonSerializer.Serialize(data, this.JsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await this.Client.PutAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var deserializedData = string.IsNullOrEmpty(responseContent)
                ? default
                : JsonSerializer.Deserialize<TResponse>(responseContent, this.JsonOptions);
            
            return (response, deserializedData);
        }

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        /// <param name="url">The request URL.</param>
        /// <returns>The HTTP response.</returns>
        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await this.Client.DeleteAsync(url);
        }

        /// <summary>
        /// Sets the authorization header for subsequent requests.
        /// </summary>
        /// <param name="token">The bearer token.</param>
        protected void SetAuthorizationHeader(string token)
        {
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Clears the authorization header.
        /// </summary>
        protected void ClearAuthorizationHeader()
        {
            this.Client.DefaultRequestHeaders.Authorization = null;
        }

        public void Dispose()
        {
            this.Client?.Dispose();
            this.Factory?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
