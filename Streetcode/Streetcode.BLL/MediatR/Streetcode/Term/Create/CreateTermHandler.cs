using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using TermEntity = Streetcode.DAL.Entities.Streetcode.TextContent.Term;

namespace Streetcode.BLL.MediatR.Term.Create;

public class CreateTermHandler : IRequestHandler<CreateTermCommand, Result<TermDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _wrapper;

    public CreateTermHandler(IMapper mapper, IRepositoryWrapper wrapper)
    {
        _mapper = mapper;
        _wrapper = wrapper;
    }

    public async Task<Result<TermDto>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
    {
        var newTerm = _mapper.Map<TermEntity>(request.Term);

        await _wrapper.TermRepository.CreateAsync(newTerm);

        if (await _wrapper.SaveChangesAsync() <= 0)
        {
            return Result.Fail("Помилка при збереженні терміну");
        }

        return Result.Ok(_mapper.Map<TermDto>(newTerm));
    }
}