namespace Streetcode.XUnitTest.MediatR.Partners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Partners;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Partners.GetAll;
    using Streetcode.DAL.Entities.Partners;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class CreatePartnerHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly GetAllPartnersHandler _handler;

        public CreatePartnerHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerService>();
            
            _handler = new GetAllPartnersHandler(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        // Tests will go here
    }
}