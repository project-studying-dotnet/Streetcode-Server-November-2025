using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.RelatedTerm.Update
{
    public class UpdateRelatedTermHandler : IRequestHandler<UpdateRelatedTermCommand, Result<RelatedTermDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerService _logger;

        public UpdateRelatedTermHandler(IMapper mapper, IRepositoryWrapper repository, ILoggerService logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<RelatedTermDto>> Handle(
            UpdateRelatedTermCommand request,
            CancellationToken cancellationToken)
        {
            var existingTerm = await _repository.RelatedTermRepository.GetFirstOrDefaultAsync(predicate: rt =>
                rt.Id == request.id && rt.Id == request.RelatedTerm.Id);

            if (existingTerm is null)
            {
                var errorMsg = ErrorMessages.RelatedTermNotFound;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _mapper.Map(request.RelatedTerm, existingTerm);
            _repository.RelatedTermRepository.Update(existingTerm);

            var resultIsSuccess = await _repository.SaveChangesAsync() > 0;
            var relatedTermDto = _mapper.Map<RelatedTermDto>(existingTerm);

            if (!resultIsSuccess || relatedTermDto is null)
            {
                return Result.Fail("Помилка при оновленні пов'язаного терміну");
            }

            return Result.Ok(relatedTermDto);
        }
    }
}