using MediatR;

namespace Streetcode.XUnitTest.MediatR.Base
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
    using Streetcode.BLL.Util;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Fixture;
    using System.Linq.Expressions;
    using System.Text.Json;

    public class StreetcodeHandlersTestsBase
    {
        protected Mock<IRepositoryWrapper> _repositoryMock;

        protected Mock<IMapper> _mapperMock;

        protected Mock<ILoggerService> _loggerMock;

        protected Mock<IMediator> _mediatorMock;

        protected StreetcodeHandlersTestsBase()
        {
            this._repositoryMock = new Mock<IRepositoryWrapper>();
            this._mapperMock = new Mock<IMapper>();
            this._loggerMock = new Mock<ILoggerService>();
            this._mediatorMock = new Mock<IMediator>();
        }

        protected void SetupMappers(CreateUpdateStreetcodeDto streetcodeDto, StreetcodeContent streetcode)
        {
            this._mapperMock
                .Setup(mapper => mapper.Map<StreetcodeContent>(It.IsAny<CreateStreetcodeDto>()))
                .Returns(streetcode);

            this._mapperMock
                .Setup(mapper => mapper.Map<CreateUpdateStreetcodeDto>(streetcode))
                .Returns(streetcodeDto);

            this._mapperMock
                .Setup(mapper => mapper.Map<ImageDetails>(It.IsAny<ImageDetailsDto>()))
                .Returns((ImageDetailsDto img) => new ImageDetails()
                {
                    ImageId = img.ImageId,
                });
        }

        protected void SetupCreateStreetcodeAsync(StreetcodeContent streetcode)
        {
            this._repositoryMock
                .Setup(r => r.StreetcodeRepository
                .CreateAsync(It.IsAny<StreetcodeContent>()))
                .ReturnsAsync(streetcode);
        }

        protected void SetupAudioRepoMocks()
        {
            this._repositoryMock
                .Setup(r => r.AudioRepository
                .GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Audio, bool>>>(),
                It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
                .ReturnsAsync((
                    Expression<Func<Audio, bool>> predicate,
                    Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>> include) =>
                {
                    var compiled = predicate.Compile();

                    var fakeDb = new List<Audio>
                    {
                        new Audio { Id = 7 },
                    };

                    return fakeDb.FirstOrDefault(compiled);
                });
            this._repositoryMock
                .Setup(r => r.AudioRepository
                .Delete(It.IsAny<Audio>()));
        }

        protected void SetupImageRepoMocks()
        {
            this._repositoryMock
                .Setup(r => r.ImageRepository
                .GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Image, bool>>>(),
                It.IsAny<Func<IQueryable<Image>, IIncludableQueryable<Image, object>>>()))
                .ReturnsAsync((
                    Expression<Func<Image, bool>> predicate,
                    Func<IQueryable<Image>, IIncludableQueryable<Image, object>> include) =>
                {
                    var compiled = predicate.Compile();

                    var fakeDb = new List<Image>
                    {
                        new Image { Id = 10 },
                        new Image { Id = 15 },
                    };

                    return fakeDb.FirstOrDefault(compiled);
                });

            this._repositoryMock
                .Setup(r => r.ImageDetailsRepository
                .CreateAsync(It.IsAny<ImageDetails>()))
                .ReturnsAsync((ImageDetails img) => img);

            this._repositoryMock
                .Setup(r => r.StreetcodeImageRepository
                .CreateAsync(It.IsAny<StreetcodeImage>()))
                .ReturnsAsync((StreetcodeImage si) => si);
        }

        protected void SetupTagsRepositoryMocks()
        {
            this._repositoryMock
                .Setup(r => r.TagRepository
                .GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Tag, bool>>>(),
                It.IsAny<Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>>()))
                .ReturnsAsync((
                    Expression<Func<Tag, bool>> predicate,
                    Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>> include) =>
                {
                    var compiled = predicate.Compile();

                    var fakeDb = new List<Tag>
                    {
                        new Tag { Id = 15 },
                        new Tag { Id = 20 },
                    };

                    return fakeDb.FirstOrDefault(compiled);
                });

            this._repositoryMock
                .Setup(r => r.StreetcodeTagIndexRepository
                .CreateAsync(It.IsAny<StreetcodeTagIndex>()))
                .ReturnsAsync((StreetcodeTagIndex si) => si);
        }

        // DeleteStreetcodeCommand Helpers
        protected void SetMocksForDelete()
        {
            var streetcodeTagIndexRepoMock = new Mock<IStreetcodeTagIndexRepository>();
            var streetcodeImageRepoMock = new Mock<IStreetcodeImageRepository>();
            var imageDetailsRepoMock = new Mock<IImageDetailsRepository>();

            this._repositoryMock
                .Setup(r => r.StreetcodeTagIndexRepository)
                .Returns(streetcodeTagIndexRepoMock.Object);

            this._repositoryMock
                .Setup(r => r.StreetcodeImageRepository)
                .Returns(streetcodeImageRepoMock.Object);

            this._repositoryMock
                .Setup(r => r.ImageDetailsRepository)
                .Returns(imageDetailsRepoMock.Object);

            streetcodeTagIndexRepoMock.SetupGetAllAsync(new List<StreetcodeTagIndex>()
            {
                new StreetcodeTagIndex { TagId = 15, StreetcodeId = 1 },
                new StreetcodeTagIndex { TagId = 20, StreetcodeId = 1 },
            });

            streetcodeImageRepoMock.SetupGetAllAsync(new List<StreetcodeImage>()
            {
                new StreetcodeImage { StreetcodeId = 1, ImageId = 10 },
                new StreetcodeImage { StreetcodeId = 1, ImageId = 15 },
            });

            imageDetailsRepoMock.SetupGetAllAsync(new List<ImageDetails>()
            {
                new ImageDetails { Id = 1, ImageId = 10 },
                new ImageDetails { Id = 2, ImageId = 15 },
            });

            streetcodeTagIndexRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<StreetcodeTagIndex>>()));

            streetcodeImageRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<StreetcodeImage>>()));

            imageDetailsRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<ImageDetails>>()));
        }

        // UpdateStreetcodeCommand Helpers
        protected UpdateStreetcodeCommand PrepareValidRequest(string json = null)
        {
            json ??= StreetcodeTestData.CreatePersonStreetcode();

            using var doc = JsonDocument.Parse(json);
            return new UpdateStreetcodeCommand(1, doc.RootElement.Clone());
        }

        protected void SetupSuccessfulUpdate(UpdateStreetcodeCommand request)
        {
            var dto = new StreetcodeCreateHelper(_loggerMock.Object)
                .ChoseStreetcodeType(
                    request.rawJsonUpdateDTO.GetProperty("StreetcodeType").GetString(),
                    request);

            var entity = new StreetcodeContent
            {
                Id = 1,
                Index = dto.Index,
                Title = dto.Title,
                TransliterationUrl = dto.TransliterationUrl
            };

            this.SetupMappers(dto, entity);
            this.SetMocksForDelete();
            this.SetupAudioRepoMocks();
            this.SetupImageRepoMocks();
            this.SetupTagsRepositoryMocks();

            this._repositoryMock.SetupSaveChangesAsync();

            this.SetupStreetcodeExists(entity);
        }

        protected void SetupStreetcodeExists(StreetcodeContent entity = null)
        {
            entity ??= new StreetcodeContent { Id = 1, Index = 1 };

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(entity);

            streetcodeRepoMock
                .Setup(r => r.Update(It.IsAny<StreetcodeContent>()));
        }

        protected void SetupStreetcodeNotFound()
        {
            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync((StreetcodeContent)null);
        }
    }
}