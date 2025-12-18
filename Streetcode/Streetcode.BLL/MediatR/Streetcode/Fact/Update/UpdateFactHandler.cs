using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
    public class UpdateFactHandler : IRequestHandler<UpdateFactCommand, Result<FactDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<FactDto>> Handle(UpdateFactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingFact =
                    await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f =>
                        f.Id == request.updateFact.Id);

                if (existingFact is null)
                {
                    string errorMsg = ErrorMessages.FactNotFound;
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                if (existingFact.ImageId != request.updateFact.ImageId)
                {
                    var imageExists =
                        await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(img =>
                            img.Id == request.updateFact.ImageId);

                    if (imageExists is null)
                    {
                        string errorMsg = ErrorMessages.ImageNotFound;
                        _logger.LogError(request, errorMsg);
                        return Result.Fail(errorMsg);
                    }
                }

                if (existingFact.Title != request.updateFact.Title)
                {
                    var duplicateTitle =
                        await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f =>
                            f.Title == request.updateFact.Title);

                    if (duplicateTitle is not null)
                    {
                        string errorMsg = ErrorMessages.FactTitleAlreadyExists;
                        _logger.LogError(request, errorMsg);
                        return Result.Fail(errorMsg);
                    }
                }

                _mapper.Map(request.updateFact, existingFact);

                _repositoryWrapper.FactRepository.Update(existingFact);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(_mapper.Map<FactDto>(existingFact));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(new Error(ex.Message));
            }
        }
    }
}
