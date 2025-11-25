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
    using Streetcode.BLL.DTO.Partners.Create;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Partners.Update;
    using Streetcode.DAL.Entities.Partners;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class UpdatePartnerHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly UpdatePartnerHandler _handler;

        public UpdatePartnerHandlerTests()
        {
            this._mockRepository = new Mock<IRepositoryWrapper>();
            this._mockMapper = new Mock<IMapper>();
            this._mockLogger = new Mock<ILoggerService>();
            this._handler = new UpdatePartnerHandler(
                this._mockRepository.Object,
                this._mockMapper.Object,
                this._mockLogger.Object);
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Once);

            this._mockRepository.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            var query = new UpdatePartnerQuery(updatePartnerDTO);

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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockRepository
                .Setup(repo => repo.SaveChanges())
                .Throws(new Exception(exceptionMessage));

            var query = new UpdatePartnerQuery(updatePartnerDTO);

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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partnerEntity);

            this._mockRepository
                .Setup(repo => repo.PartnerSourceLinkRepository.GetAllAsync(
                    It.IsAny<Expression<Func<PartnerSourceLink, bool>>>(),
                    It.IsAny<Func<IQueryable<PartnerSourceLink>, IIncludableQueryable<PartnerSourceLink, object>>>()))
                .ReturnsAsync(existingLinks);

            this._mockRepository
                .Setup(repo => repo.PartnerStreetcodeRepository.GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodePartner, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodePartner>, IIncludableQueryable<StreetcodePartner, object>>>()))
                .ReturnsAsync(oldStreetcodes);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partnerEntity))
                .Returns(resultPartnerDTO);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockMapper.Verify(
                mapper => mapper.Map<Partner>(updatePartnerDTO),
                Times.Once);
            this._mockMapper.Verify(
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

            this._mockMapper
                .Setup(mapper => mapper.Map<Partner>(updatePartnerDTO))
                .Returns((Partner)null);

            var query = new UpdatePartnerQuery(updatePartnerDTO);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.Update(It.IsAny<Partner>()),
                Times.Never);

            this._mockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Never);
        }
    }
}