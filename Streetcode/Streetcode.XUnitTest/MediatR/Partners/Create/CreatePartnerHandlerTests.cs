using System.Linq.Expressions;
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
    /// <summary>
    /// Unit tests for <see cref="CreatePartnerHandler"/>.
    /// </summary>
    public class CreatePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly CreatePartnerHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerHandlerTests"/> class.
        /// </summary>
        public CreatePartnerHandlerTests()
        {
            this._handler = new CreatePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <summary>
        /// Sets up the mapper to map CreatePartnerDto to Partner entity.
        /// </summary>
        /// <param name="createPartnerDTO">The DTO to map from.</param>
        /// <param name="partnerEntity">The entity to map to.</param>
        private void SetupMapperForCreatePartner(CreatePartnerDtoo createPartnerDTO, Partner partnerEntity)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns(partnerEntity);
        }

        /// <summary>
        /// Sets up the repository to return a partner when CreateAsync is called.
        /// </summary>
        /// <param name="partnerEntity">The partner entity to return.</param>
        private void SetupCreateAsync(Partner partnerEntity)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ReturnsAsync(partnerEntity);
        }

        /// <summary>
        /// Sets up the streetcode repository to return specific streetcodes.
        /// </summary>
        /// <param name="streetcodes">The streetcodes to return.</param>
        private void SetupStreetcodeRepository(List<StreetcodeContent> streetcodes)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcodes);
        }

        /// <summary>
        /// Sets up CreateAsync to throw an exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupCreateAsyncToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.CreateAsync(It.IsAny<Partner>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Sets up the mapper to return null when mapping CreatePartnerDto to Partner.
        /// </summary>
        /// <param name="createPartnerDTO">The DTO being mapped.</param>
        private void SetupMapperToReturnNullPartner(CreatePartnerDtoo createPartnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(createPartnerDTO))
                .Returns((Partner)null);
        }

        /// <summary>
        /// Sets up the streetcode repository to throw an exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupStreetcodeRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Verifies that the handler returns success when a partner is created successfully with streetcodes.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerCreatedSuccessfully()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Test Description",
                Streetcodes = new List<StreetcodeShortDto>
                {
                    new StreetcodeShortDto { Id = 1 },
                    new StreetcodeShortDto { Id = 2 },
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
                repo => repo.SaveChangesAsync(),
                Times.Exactly(2));
        }

        /// <summary>
        /// Verifies that the handler returns success when a partner is created without streetcodes.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerCreatedWithoutStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Test Description",
                Streetcodes = new List<StreetcodeShortDto>(),
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

        /// <summary>
        /// Verifies that the handler calls the mapper when creating a partner.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsMapper_WhenCreatingPartner()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
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
                mapper => mapper.Map<PartnerDtoo>(partnerEntity),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler returns failure when an exception occurs during creation.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenExceptionOccurs()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
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

        /// <summary>
        /// Verifies that the handler returns failure when SaveChanges throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveChangesThrowsException()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
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

        /// <summary>
        /// Verifies that the handler calls SaveChanges twice when a partner has streetcodes.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsSaveChangesTwice_WhenPartnerHasStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>
                {
                    new StreetcodeShortDto { Id = 1 },
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
                repo => repo.SaveChangesAsync(),
                Times.Exactly(2),
                "because SaveChanges should be called after creating partner and after adding streetcodes");
        }

        /// <summary>
        /// Verifies that the handler retrieves streetcodes by their IDs when creating a partner.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_RetrievesStreetcodesByIds_WhenPartnerHasStreetcodes()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>
                {
                    new StreetcodeShortDto { Id = 10 },
                    new StreetcodeShortDto { Id = 20 },
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

            Expression<Func<StreetcodeContent, bool>> capturedPredicate = null;
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .Callback<Expression<Func<StreetcodeContent, bool>>, Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>(
                    (pred, include) => capturedPredicate = pred)
                .ReturnsAsync(streetcodes);

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

        /// <summary>
        /// Verifies that the handler returns failure when the mapper returns null for the partner entity.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenMapperReturnsNullForPartnerEntity()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
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

        /// <summary>
        /// Verifies that the handler returns failure when the streetcode repository throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenStreetcodeRepositoryThrowsException()
        {
            // Arrange
            var createPartnerDTO = new CreatePartnerDtoo
            {
                Id = 1,
                Title = "New Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>
                {
                    new StreetcodeShortDto { Id = 1 },
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
