using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.DTO.Streetcode.Types;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;

namespace Streetcode.BLL.Util
{
    public class StreetcodeCreateHelper
    {
        private readonly ILoggerService _logger;
        public StreetcodeCreateHelper(ILoggerService logger)
        {
            _logger = logger;
        }

        public CreateStreetcodeDto ChoseStreetcodeType(string type, CreateStreetcodeCommand request)
        {
            switch (type)
            {
                case "Person":
                    return JsonSerializer
                        .Deserialize<CreatePersonStreetcodeDto>(request.rawJsonCreateDTO.GetRawText());
                case "Event":
                    return JsonSerializer
                        .Deserialize<CreateEventStreetcodeDto>(request.rawJsonCreateDTO.GetRawText());
                default:
                    var errorMsg = ErrorMessages.StreetcodeCreationFailed;
                    _logger.LogError(request, errorMsg);
                    throw new InvalidOperationException(errorMsg);
            }
        }

        public UpdateStreetcodeDto ChoseStreetcodeType(string type, UpdateStreetcodeCommand request)
        {
            switch (type)
            {
                case "Person":
                    return JsonSerializer
                        .Deserialize<UpdatePersonStreetcodeDto>(request.rawJsonUpdateDTO.GetRawText());
                case "Event":
                    return JsonSerializer
                        .Deserialize<UpdateEventStreetcodeDto>(request.rawJsonUpdateDTO.GetRawText());
                default:
                    var errorMsg = ErrorMessages.StreetcodeCreationFailed;
                    _logger.LogError(request, errorMsg);
                    throw new InvalidOperationException(errorMsg);
            }
        }
    }
}
