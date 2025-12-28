using Microsoft.Extensions.Configuration;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.WebApi.Extensions
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILoggerService _loggerService;

        public BackgroundWorker(
            IServiceScopeFactory serviceScopeFactory,
            ILoggerService loggerService,
            IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _loggerService = loggerService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var refreshTokenAutoDeleteTime = jwtSettings["RefreshTokenAutoDeleteTime"];

            while (!stoppingToken.IsCancellationRequested)
            {
                await DeleteExpieredRefreshTokens();

                await Task.Delay(
                    TimeSpan.FromMinutes(
                        Convert.ToInt32(refreshTokenAutoDeleteTime)), stoppingToken);
            }
        }

        private async Task DeleteExpieredRefreshTokens()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IRepositoryWrapper>();

                var expieredTokens = await repository.RefreshTokenRepository
                    .GetAllAsync(t => t.ExpiresOn < DateTime.UtcNow || t.IsRevoked);

                if (expieredTokens.Any())
                {
                    repository.RefreshTokenRepository.DeleteRange(expieredTokens);
                    await repository.SaveChangesAsync();
                }

                _loggerService
                    .LogInformation($"Deleted {expieredTokens.Count()} expiered tokens");
            }
        }
    }
}
