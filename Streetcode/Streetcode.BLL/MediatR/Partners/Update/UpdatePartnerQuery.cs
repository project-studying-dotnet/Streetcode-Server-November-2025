using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Partners.Create;

namespace Streetcode.BLL.MediatR.Partners.Update
{
  public record UpdatePartnerQuery(CreatePartnerDtoo Partner) : IRequest<Result<PartnerDtoo>>;
}
