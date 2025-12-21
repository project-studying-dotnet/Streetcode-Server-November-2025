using FluentResults;
using MediatR;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.Logout
{
	public class LogoutHandler : IRequestHandler<LogoutCommand, Result<Unit>>
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public LogoutHandler(IRepositoryWrapper repositoryWrapper)
        {
			_repositoryWrapper = repositoryWrapper;
		}

		public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			var refreshToken = await _repositoryWrapper.RefreshTokenRepository
				.GetFirstOrDefaultAsync(rt => rt.Token == request.LogoutRequestDto.RefreshToken);

			if (refreshToken == null)
			{
				return Result.Fail("Refresh token not found or already invalid.");
			}

			_repositoryWrapper.RefreshTokenRepository.Delete(refreshToken);

			var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

			if (!resultIsSuccess)
			{
				return Result.Fail("Failed to logout user.");
			}

			return Result.Ok(Unit.Value);
		}
    }
}
