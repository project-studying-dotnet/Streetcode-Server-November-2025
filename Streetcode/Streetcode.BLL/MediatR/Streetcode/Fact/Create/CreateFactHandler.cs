using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Fact.Create
{
    using DAL.Entities.Streetcode.TextContent;
    using Microsoft.EntityFrameworkCore;

    public class CreateFactHandler : IRequestHandler<CreateFactCommand, Result<FactDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<FactDto>> Handle(CreateFactCommand request, CancellationToken cancellationToken)
        {
            var imageExists =
                await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(img =>
                    img.Id == request.newFact.ImageId);
            if (imageExists is null)
            {
                var errorMsg = string.Format(ErrorMessages.ImageNotFoundById, request.newFact.ImageId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcodeExists =
                await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(s =>
                    s.Id == request.newFact.StreetcodeId);
            if (streetcodeExists is null)
            {
                var errorMsg = string.Format(ErrorMessages.StreetcodeNotFoundById, request.newFact.StreetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var factExists =
                await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f =>
                    f.Title == request.newFact.Title &&
                    f.StreetcodeId == request.newFact.StreetcodeId);
            if (factExists is not null)
            {
                var errorMsg = ErrorMessages.FactTitleAlreadyExists;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var newFact = _mapper.Map<Fact>(request.newFact);
            if (newFact is null)
            {
                var errorMsg = ErrorMessages.FactMappingFailed;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var lastFactOrderPosition = await _repositoryWrapper.FactRepository
                .FindAll(f => f.StreetcodeId == request.newFact.StreetcodeId)
                .MaxAsync(f => (int?)f.Order, CancellationToken.None) ?? 0;

            newFact.Order = lastFactOrderPosition + 1;

            newFact = await _repositoryWrapper.FactRepository.CreateAsync(newFact);
            await _repositoryWrapper.SaveChangesAsync();
            return Result.Ok(_mapper.Map<FactDto>(newFact));
        }
    }
}