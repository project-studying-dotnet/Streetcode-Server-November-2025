using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.MediatR.Partners.Create;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class CreatePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly CreatePartnerHandler _handler;

        public CreatePartnerHandlerTests()
        {
            this._handler = new CreatePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        private void SetupMapperForCreatePartner(CreatePartnerDTO createPartnerDTO, Partner partnerEntity)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);
        }

        private void SetupCreateAsync(Partner partnerEntity)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);
        }

        private void SetupStreetcodeRepository(List<StreetcodeContent> streetcodes)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcodes);
        }

        private Expression<Func<StreetcodeContent, bool>> CaptureStreetcodePredicate()
        {
            Expression<Func<StreetcodeContent, bool>> capturedPredicate = null;
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .Callback<Expression<Func<StreetcodeContent, bool>>, Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>(
                    (predicate, include) => capturedPredicate = predicate)
                .ReturnsAsync(new List<StreetcodeContent>());
            return capturedPredicate;
        }

        private void SetupCreateAsyncToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ThrowsAsync(exception);
        }

        private void SetupMapperToReturnNullPartner(CreatePartnerDTO createPartnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns((Partner)null);
        }

        private void SetupStreetcodeRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ThrowsAsync(exception);
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupStreetcodeRepository(streetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(resultPartnerDTO);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()),
                Times.Once);

            this.MockRepository.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupStreetcodeRepository(new List<StreetcodeContent>());
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupStreetcodeRepository(new List<StreetcodeContent>());
            this.SetupMapperForSpecificPartner(partnerEntity, resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<Partner>(createPartnerDTO),
                Times.Once);
            this.MockMapper.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsyncToThrowException(new Exception(exceptionMessage));

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this.MockLogger.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupSaveChangesToThrowException(exceptionMessage);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this.MockLogger.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupStreetcodeRepository(streetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            var capturedPredicate = this.CaptureStreetcodePredicate();
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull("because predicate should be provided for filtering streetcodes");

            this.MockRepository.Verify(
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

            this.SetupMapperToReturnNullPartner(createPartnerDTO);

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();

            this.MockLogger.Verify(
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

            this.SetupMapperForCreatePartner(createPartnerDTO, partnerEntity);
            this.SetupCreateAsync(partnerEntity);
            this.SetupStreetcodeRepositoryToThrowException(new Exception(exceptionMessage));

            var query = new CreatePartnerQuery(createPartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    exceptionMessage),
                Times.Once);
        }
    }
}

