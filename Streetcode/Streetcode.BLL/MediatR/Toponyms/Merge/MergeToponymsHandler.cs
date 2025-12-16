using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Toponyms;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Toponyms.Merge
{
    public class MergeToponymsHandler : IRequestHandler<MergeToponymsCommand, Result<ToponymDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public MergeToponymsHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ToponymDto>> Handle(
        MergeToponymsCommand request,
        CancellationToken cancellationToken)
        {
            var targetToponym = await _repositoryWrapper.ToponymRepository
                .GetFirstOrDefaultAsync(t => t.Id == request.MergeRequest.TargetToponymId);

            if (targetToponym is null)
            {
                string errorMsg = $"Target toponym with Id={request.MergeRequest.TargetToponymId} not found.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            using var transaction = _repositoryWrapper.BeginTransaction();

            try
            {
                foreach (var sourceId in request.MergeRequest.SourceToponymIds)
                {
                    var sourceRelationships = await _repositoryWrapper.StreetcodeToponymRepository
                        .GetAllAsync(st => st.ToponymId == sourceId);

                    if (sourceRelationships is not null)
                    {
                        foreach (var relationship in sourceRelationships)
                        {
                            var existingRelation = await _repositoryWrapper.StreetcodeToponymRepository
                                .GetFirstOrDefaultAsync(st =>
                                    st.ToponymId == request.MergeRequest.TargetToponymId &&
                                    st.StreetcodeId == relationship.StreetcodeId);

                            if (existingRelation is null)
                            {
                                await _repositoryWrapper.StreetcodeToponymRepository.CreateAsync(
                                    new DAL.Entities.Toponyms.StreetcodeToponym
                                    {
                                        StreetcodeId = relationship.StreetcodeId,
                                        ToponymId = request.MergeRequest.TargetToponymId
                                    });
                            }

                            _repositoryWrapper.StreetcodeToponymRepository.Delete(relationship);
                        }
                    }

                    var sourceToponym = await _repositoryWrapper.ToponymRepository
                        .GetFirstOrDefaultAsync(t => t.Id == sourceId);

                    if (sourceToponym is not null)
                    {
                        _repositoryWrapper.ToponymRepository.Delete(sourceToponym);
                    }
                }

                await _repositoryWrapper.SaveChangesAsync();
                transaction.Complete();

                return Result.Ok(_mapper.Map<ToponymDto>(targetToponym));
            }
            catch (Exception ex)
            {
                string errorMsg = $"Failed to merge toponyms: {ex.Message}";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
