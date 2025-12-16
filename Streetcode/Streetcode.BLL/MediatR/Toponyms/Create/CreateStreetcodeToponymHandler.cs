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
                const string errorMsg = "This toponym is already linked to the streetcode.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var streetcodeToponym = _mapper.Map<DAL.Entities.Toponyms.StreetcodeToponym>(request.StreetcodeToponym);

            if (streetcodeToponym is null)
            {
                const string errorMsg = "Cannot map StreetcodeToponymDto to entity.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            await _repositoryWrapper.StreetcodeToponymRepository.CreateAsync(streetcodeToponym);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (!resultIsSuccess)
            {
                const string errorMsg = "Failed to create streetcode-toponym relationship.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<StreetcodeToponymDto>(streetcodeToponym));
        }
    }
}