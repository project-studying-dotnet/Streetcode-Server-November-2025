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
				.GetFirstOrDefaultAsync(rt =>
					rt.Token == request.LogoutRequest.RefreshToken &&
				    rt.UserId == request.UserId);

			if (refreshToken is null)
			{
				return Result.Ok(Unit.Value);
			}

			_repositoryWrapper.RefreshTokenRepository.Delete(refreshToken);

			await _repositoryWrapper.SaveChangesAsync();

			return Result.Ok(Unit.Value);
		}
    }
}
