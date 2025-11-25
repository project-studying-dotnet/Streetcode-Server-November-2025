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
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Partners.Create;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class CreatePartnerHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly CreatePartnerHandler _handler;

        public CreatePartnerHandlerTests()
        {
            this._mockRepository = new Mock<IRepositoryWrapper>();
            this._mockMapper = new Mock<IMapper>();
            this._mockLogger = new Mock<ILoggerService>();
            this._handler = new CreatePartnerHandler(
                this._mockRepository.Object,
                this._mockMapper.Object,
                this._mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerCreatedSuccessfully()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Test Description",
                Streetcodes = new List<StreetcodeShortDTO>
                {
                    new StreetcodeShortDTO { Id = 1 },
                    new StreetcodeShortDTO { Id = 2 },
                },
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var streetcodes = new List<StreetcodeContent>
            {
                new StreetcodeContent { Id = 1 },
                new StreetcodeContent { Id = 2 },
            };
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultPartnerDTO);

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()),
                Times.Once);

            this._mockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerCreatedWithoutStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Test Description",
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(new List<StreetcodeContent>());

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultPartnerDTO);
        }

        [Fact]
        public async Task Handle_CallsMapper_WhenCreatingPartner()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(new List<StreetcodeContent>());

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partnerEntity))
                .Returns(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockMapper.Verify(
                mapper => mapper.Map<Partner>(createPartnerDTO),
                Times.Once);
            this._mockMapper.Verify(
                mapper => mapper.Map<PartnerDTO>(partnerEntity),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenExceptionOccurs()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var exceptionMessage = "Database error occurred";

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this._mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    exceptionMessage),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveChangesThrowsException()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var exceptionMessage = "Save changes failed";

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.SaveChanges())
                .Throws(new Exception(exceptionMessage));

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this._mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    exceptionMessage),
                Times.Once);
        }

        [Fact]
        public async Task Handle_CallsSaveChangesTwice_WhenPartnerHasStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>
                {
                    new StreetcodeShortDTO { Id = 1 },
                },
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = 1 } };
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Exactly(2),
                "because SaveChanges should be called after creating partner and after adding streetcodes");
        }

        [Fact]
        public async Task Handle_RetrievesStreetcodesByIds_WhenPartnerHasStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>
                {
                    new StreetcodeShortDTO { Id = 10 },
                    new StreetcodeShortDTO { Id = 20 },
                },
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var streetcodes = new List<StreetcodeContent>
            {
                new StreetcodeContent { Id = 10 },
                new StreetcodeContent { Id = 20 },
            };
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);
            Expression<Func<StreetcodeContent, bool>> capturedPredicate = null;

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .Callback<Expression<Func<StreetcodeContent, bool>>, Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>(
                    (predicate, include) => capturedPredicate = predicate)
                .ReturnsAsync(streetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull("because predicate should be provided for filtering streetcodes");

            this._mockRepository.Verify(
                repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenMapperReturnsNullForPartnerEntity()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns((Partner)null);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();

            this._mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenStreetcodeRepositoryThrowsException()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>
                {
                    new StreetcodeShortDTO { Id = 1 },
                },
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var exceptionMessage = "Streetcode repository error";

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this._mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    exceptionMessage),
                Times.Once);
        }
    }
}