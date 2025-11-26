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
using Streetcode.BLL.DTO.Partners.Create;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.MediatR.Partners.Update;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class UpdatePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly UpdatePartnerHandler _handler;

        public UpdatePartnerHandlerTests()
        {
            this._handler = new UpdatePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        private void SetupMapperForUpdatePartner(CreatePartnerDTO updatePartnerDTO, Partner partnerEntity)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);
        }

        private void SetupGetFirstOrDefaultAsync(Partner partnerEntity)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);
        }

        private void SetupPartnerSourceLinkRepository(List<PartnerSourceLink> existingLinks)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);
        }

        private void SetupPartnerStreetcodeRepository(List<StreetcodePartner> oldStreetcodes)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);
        }

        private void SetupPartnerSourceLinkRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ThrowsAsync(exception);
        }

        private void SetupMapperToReturnNullPartner(CreatePartnerDTO updatePartnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns((Partner)null);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerUpdatedSuccessfully()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Description = "Updated Description",
                Streetcodes = new List<StreetcodeShortDTO>
                {
                    new StreetcodeShortDTO { Id = 1 },
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Once);

            this.MockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_DeletesOldLinks_WhenLinksAreRemoved()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerSourceLinkRepository.Delete(It.IsAny<PartnerSourceLink>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_CreatesNewStreetcodeLinks_WhenStreetcodesAreAdded()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
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
            partnerEntity.PartnerSourceLinks = new List<PartnerSourceLink>();
            var existingLinks = new List<PartnerSourceLink>();
            var oldStreetcodes = new List<StreetcodePartner>();
            var resultPartnerDTO = PartnerTestHelpers.CreatePartnerDTO(1);

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupGetFirstOrDefaultAsync(partnerEntity);
            this.SetupPartnerSourceLinkRepository(existingLinks);
            this.SetupPartnerStreetcodeRepository(oldStreetcodes);
            this.SetupMapperForPartnerDTO(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerStreetcodeRepository.Create(It.IsAny<StreetcodePartner>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_DeletesOldStreetcodeLinks_WhenStreetcodesAreRemoved()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnerStreetcodeRepository.Delete(It.IsAny<StreetcodePartner>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_CallsSaveChangesTwice_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Exactly(2),
                "because SaveChanges should be called after updating partner and after modifying streetcode links");
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenExceptionOccurs()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            var partnerEntity = PartnerTestHelpers.CreatePartnerEntity(1);
            var exceptionMessage = "Database error";

            this.SetupMapperForUpdatePartner(updatePartnerDTO, partnerEntity);
            this.SetupPartnerSourceLinkRepositoryToThrowException(new Exception(exceptionMessage));

            var query = new UpdatePartnerQuery(updatePartnerDTO);

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
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

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
        public async Task Handle_CallsMapper_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
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

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<Partner>(updatePartnerDTO),
                Times.Once);
            this.MockMapper.Verify(
                mapper => mapper.Map<PartnerDTO>(partnerEntity),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenMapperReturnsNullForPartnerEntity()
        {
            // Arrange
            var updatePartnerDTO = new CreatePartnerDTO
            {
                Id = 1,
                Title = "Updated Partner",
                IsKeyPartner = true,
                IsVisibleEverywhere = false,
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDTO>(),
            };

            this.SetupMapperToReturnNullPartner(updatePartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Never);

            this.MockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Never);
        }
    }
}

