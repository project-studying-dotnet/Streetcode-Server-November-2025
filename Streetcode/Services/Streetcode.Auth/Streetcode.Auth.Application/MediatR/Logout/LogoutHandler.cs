using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.BuildingBlocks.Interfaces.Logging;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Streetcode.Auth.Application.MediatR.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, Result<Unit>>
    {
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutHandler(ITokenService tokenService, ILoggerService logger, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.LogoutRequestDto is null)
                {
                    _logger.LogError(request, "ErrorMessages.LogoutFailure");
                    return Result.Fail<Unit>("ErrorMessages.LogoutFailure");
                }

                var refreshToken = request.LogoutRequestDto.RefreshToken;

                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    _logger.LogError(request, "ErrorMessages.RefreshTokenInvalid");
                    return Result.Fail<Unit>("ErrorMessages.RefreshTokenInvalid");
                }

                var principal = _httpContextAccessor.HttpContext?.User;

                var userIdText =
                    principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                    principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (!int.TryParse(userIdText, out var userId) || userId <= 0)
                {
                    _logger.LogError(request, "ErrorMessages.Unauthorized");
                    return Result.Fail<Unit>("ErrorMessages.Unauthorized");
                }

                await _tokenService.RevokeRefreshTokenAsync(refreshToken, userId, cancellationToken);

                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ErrorMessages.LogoutFailure");
                return Result.Fail<Unit>("ErrorMessages.LogoutFailure");
            }
        }
    }
}
