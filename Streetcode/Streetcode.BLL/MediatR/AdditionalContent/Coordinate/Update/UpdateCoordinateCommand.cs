using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;

public record UpdateCoordinateCommand(StreetcodeCoordinateDtoo StreetcodeCoordinate) : IRequest<Result<Unit>>;