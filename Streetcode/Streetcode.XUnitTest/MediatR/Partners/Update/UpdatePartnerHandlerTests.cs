using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.MediatR.Partners.Update;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="UpdatePartnerHandler"/>.
    /// </summary>
    public class UpdatePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly UpdatePartnerHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePartnerHandlerTests"/> class.
        /// </summary>
        public UpdatePartnerHandlerTests()
        {
            this._handler = new UpdatePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <summary>
        /// Sets up the mapper to map CreatePartnerDto to Partner entity for updates.
        /// </summary>
        /// <param name="updatePartnerDTO">The DTO containing update data.</param>
        /// <param name="partnerEntity">The entity to map to.</param>
        private void SetupMapperForUpdatePartner(CreatePartnerDto updatePartnerDTO, Partner partnerEntity)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);
        }

        /// <summary>
        /// Sets up the repository to return a partner when GetFirstOrDefaultAsync is called.
        /// </summary>
        /// <param name="partnerEntity">The partner to return.</param>
        private void SetupGetFirstOrDefaultAsync(Partner partnerEntity)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);
        }

        /// <summary>
        /// Sets up the partner source link repository to return existing links.
        /// </summary>
        /// <param name="existingLinks">The existing partner source links.</param>
        private void SetupPartnerSourceLinkRepository(List<PartnerSourceLink> existingLinks)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);
        }

        /// <summary>
        /// Sets up the partner streetcode repository to return existing streetcode relationships.
        /// </summary>
        /// <param name="oldStreetcodes">The existing streetcode-partner relationships.</param>
        private void SetupPartnerStreetcodeRepository(List<StreetcodePartner> oldStreetcodes)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);
        }

        /// <summary>
        /// Sets up the partner source link repository to throw an exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupPartnerSourceLinkRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Sets up the mapper to return null when mapping CreatePartnerDto to Partner.
        /// </summary>
        /// <param name="updatePartnerDTO">The DTO being mapped.</param>
        private void SetupMapperToReturnNullPartner(CreatePartnerDto updatePartnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns((Partner)null);
        }

        /// <summary>
        /// Verifies that the handler returns success when a partner is updated successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerUpdatedSuccessfully()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Updated Description",
                Streetcodes = new List<StreetcodeShortDto>
                {
                    new StreetcodeShortDto { Id = 1 },
                },
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Once);

            this.MockRepository.Verify(
                repo => repo.SaveChangesAsync(),
                Times.Exactly(2));
        }

        /// <summary>
        /// Verifies that the handler deletes old partner source links when they are removed during an update.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_DeletesOldLinks_WhenLinksAreRemoved()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>
            {
                new PartnerSourceLink { Id = 1, PartnerId = 1 },
                new PartnerSourceLink { Id = 2, PartnerId = 1 },
            };
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerSourceLinkRepository.Delete(It.IsAny<PartnerSourceLink>()),
                Times.Exactly(2));
        }

        /// <summary>
        /// Verifies that the handler creates new streetcode-partner links when streetcodes are added during an update.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CreatesNewStreetcodeLinks_WhenStreetcodesAreAdded()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
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
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerStreetcodeRepository.CreateAsync(It.IsAny<StreetcodePartner>()),
                Times.Exactly(2));
        }

        /// <summary>
        /// Verifies that the handler deletes old streetcode-partner links when streetcodes are removed during an update.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_DeletesOldStreetcodeLinks_WhenStreetcodesAreRemoved()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>
            {
                new StreetcodePartner { PartnerId = 1, StreetcodeId = 5 },
                new StreetcodePartner { PartnerId = 1, StreetcodeId = 6 },
            };
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerStreetcodeRepository.Delete(It.IsAny<StreetcodePartner>()),
                Times.Exactly(2));
        }

        /// <summary>
        /// Verifies that the handler calls SaveChanges twice when an update is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsSaveChangesTwice_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.SaveChangesAsync(),
                Times.Exactly(2),
                "because SaveChanges should be called after updating partner and after modifying streetcode links");
        }

        /// <summary>
        /// Verifies that the handler returns failure when an exception occurs during update.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenExceptionOccurs()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var exceptionMessage = "Database error";

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupPartnerSourceLinkRepositoryToThrowException(new Exception(exceptionMessage));

            var query = new UpdatePartnerCommand(updatePartnerDTO);

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
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var exceptionMessage = "Save changes failed";

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupSaveChangesToThrowException(exceptionMessage);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

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
        /// Verifies that the handler calls the mapper when an update is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsMapper_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForSpecificPartner(partnerEntity, resultPartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<Partner>(updatePartnerDTO),
                Times.Once);
            this.MockMapper.Verify(
                mapper => mapper.Map<PartnerDto>(partnerEntity),
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
            var updatePartnerDTO = new CreatePartnerDto
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            this.SetupMapperToReturnNullPartner(updatePartnerDTO);

            var query = new UpdatePartnerCommand(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Never);

            this.MockRepository.Verify(
                repo => repo.SaveChangesAsync(),
                Times.Never);
        }
    }
}
