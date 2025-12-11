using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Media.Images;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace Streetcode.XUnitTest.MediatR.Post
{
    public class CreateStreetcodeHandlerTestsBase
    {
        protected Mock<IRepositoryWrapper> _repositoryMock;

        protected Mock<IMapper> _mapperMock;

        protected Mock<ILoggerService> _loggerMock;

        protected Mock<IMediator> _mediatorMock;

        protected void SetupMappers(CreateStreetcodeDto streetcodeDto, StreetcodeContent streetcode)
        {
            this._mapperMock
                .Setup(mapper => mapper.Map<StreetcodeContent>(It.IsAny<CreateStreetcodeDto>()))
                .Returns(streetcode);

            this._mapperMock
                .Setup(mapper => mapper.Map<CreateStreetcodeDto>(streetcode))
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

        protected CreateStreetcodeHandlerTestsBase()
        {
            this._repositoryMock = new Mock<IRepositoryWrapper>();
            this._mapperMock = new Mock<IMapper>();
            this._loggerMock = new Mock<ILoggerService>();
            this._mediatorMock = new Mock<IMediator>();
        }
    }
}