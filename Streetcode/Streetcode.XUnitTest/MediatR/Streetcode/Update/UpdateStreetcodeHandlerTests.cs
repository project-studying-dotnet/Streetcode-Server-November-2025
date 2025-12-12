using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
using Streetcode.XUnitTest.MediatR.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.XUnitTest.MediatR.Update
{
    public class UpdateStreetcodeHandlerTests : StreetcodeHandlersTestsBase
    {
        private UpdateStreetcodeHandler handler;

        public UpdateStreetcodeHandlerTests()
        {
            this.handler = new UpdateStreetcodeHandler(
                this._repositoryMock.Object,
                this._mapperMock.Object,
                this._loggerMock.Object);
        }
    }
}
