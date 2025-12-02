using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.GetAll;

public record GetAllToponymsQuery(GetAllToponymsRequestDtoo request)
    : IRequest<Result<GetAllToponymsResponseDtoo>>;