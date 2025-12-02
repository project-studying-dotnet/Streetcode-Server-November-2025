using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Partners;

namespace Streetcode.BLL.MediatR.Partners.Create
{
  public record CreatePartnerQuery(CreatePartnerDtoo newPartner) : IRequest<Result<PartnerDtoo>>;
}
