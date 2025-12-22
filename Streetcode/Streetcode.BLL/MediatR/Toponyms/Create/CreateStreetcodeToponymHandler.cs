using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Toponyms;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Toponyms.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Toponyms.Create
{
    public class CreateStreetcodeToponymHandler
        : IRequestHandler<CreateStreetcodeToponymCommand, Result<StreetcodeToponymDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateStreetcodeToponymHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<StreetcodeToponymDto>> Handle(
            CreateStreetcodeToponymCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repositoryWrapper.StreetcodeToponymRepository
                .GetFirstOrDefaultAsync(st =>
                    st.StreetcodeId == request.StreetcodeToponym.StreetcodeId &&
                    st.ToponymId == request.StreetcodeToponym.ToponymId);

            if (existing is not null)
            {
                string errorMsg = ErrorMessages.ToponymAlreadyLinked;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var streetcodeToponym = _mapper.Map<DAL.Entities.Toponyms.StreetcodeToponym>(request.StreetcodeToponym);

            if (streetcodeToponym is null)
            {
                string errorMsg = ErrorMessages.ToponymCantBeMapped;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            await _repositoryWrapper.StreetcodeToponymRepository.CreateAsync(streetcodeToponym);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (!resultIsSuccess)
            {
                string errorMsg = ErrorMessages.ToponymStreetcodeFailedToCreate;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<StreetcodeToponymDto>(streetcodeToponym));
        }
    }
}