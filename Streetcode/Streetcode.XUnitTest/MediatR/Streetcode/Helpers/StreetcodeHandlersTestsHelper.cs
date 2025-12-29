namespace Streetcode.XUnitTest.MediatR.Base
{
    using System.Linq.Expressions;
    using System.Text.Json;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Repositories.Interfaces;
 using global::Streetcode.BLL.DTO.Media.Images;
 using global::Streetcode.BLL.DTO.Streetcode;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
 using global::Streetcode.BLL.Util;
 using global::Streetcode.DAL.Entities.AdditionalContent;
 using global::Streetcode.DAL.Entities.Media;
 using global::Streetcode.DAL.Entities.Media.Images;
 using global::Streetcode.DAL.Entities.Streetcode;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Media.Images;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Streetcode.Fixture;

    public class StreetcodeHandlersTestsHelper
    {
        private Mock<IRepositoryWrapper> repositoryMock;

        private Mock<IMapper> mapperMock;

        private Mock<ILoggerService> loggerMock;

        private Mock<IStreetcodeTagIndexRepository> streetcodeTagIndexRepoMock;
        private Mock<IStreetcodeImageRepository> streetcodeImageRepoMock;
        private Mock<IImageDetailsRepository> imageDetailsRepoMock;
        private Mock<IAudioRepository> audioRepositoryMock;
        private Mock<IStreetcodeRepository> streetcodeRepoMock;

        public StreetcodeHandlersTestsHelper(Mock<IRepositoryWrapper> repoMock, Mock<IMapper> mapperMock, Mock<ILoggerService> loggerMock)
        {
            this.repositoryMock = repoMock;
            this.mapperMock = mapperMock;
            this.loggerMock = loggerMock;

            this.streetcodeTagIndexRepoMock = new Mock<IStreetcodeTagIndexRepository>();
            this.streetcodeImageRepoMock = new Mock<IStreetcodeImageRepository>();
            this.imageDetailsRepoMock = new Mock<IImageDetailsRepository>();
            this.audioRepositoryMock = new Mock<IAudioRepository>();
            this.streetcodeRepoMock = new Mock<IStreetcodeRepository>();

        }

        public void SetupMappers(CreateUpdateStreetcodeDto streetcodeDto, StreetcodeContent streetcode)
        {
            this.mapperMock
                .Setup(mapper => mapper.Map<StreetcodeContent>(It.IsAny<CreateStreetcodeDto>()))
                .Returns(streetcode);

            this.mapperMock
                .Setup(mapper => mapper.Map<CreateUpdateStreetcodeDto>(streetcode))
                .Returns(streetcodeDto);

            this.mapperMock
                .Setup(mapper => mapper.Map<ImageDetails>(It.IsAny<ImageDetailsDto>()))
                .Returns((ImageDetailsDto img) => new ImageDetails()
                {
                    ImageId = img.ImageId,
                });
        }

        public void SetupCreateStreetcodeAsync(StreetcodeContent streetcode)
        {
            this.repositoryMock
                .Setup(r => r.StreetcodeTagIndexRepository)
                .Returns(this.streetcodeTagIndexRepoMock.Object);

            this.repositoryMock
                .Setup(r => r.StreetcodeImageRepository)
                .Returns(this.streetcodeImageRepoMock.Object);

            this.repositoryMock
                .Setup(r => r.ImageDetailsRepository)
                .Returns(this.imageDetailsRepoMock.Object);

            this.repositoryMock
                .Setup(r => r.StreetcodeRepository
                .CreateAsync(It.IsAny<StreetcodeContent>()))
                .ReturnsAsync(streetcode);

            this.repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(this.streetcodeRepoMock.Object);
        }

        public void SetupAudioRepoMocks()
        {
            this.repositoryMock
            .Setup(r => r.AudioRepository)
            .Returns(this.audioRepositoryMock.Object);

            this.audioRepositoryMock
                .Setup(r => r
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

            this.repositoryMock
                .Setup(r => r.AudioRepository
                .Delete(It.IsAny<Audio>()));
        }

        public void SetupImageRepoMocks()
        {
            this.repositoryMock
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

            this.repositoryMock
                .Setup(r => r.ImageDetailsRepository
                .CreateAsync(It.IsAny<ImageDetails>()))
                .ReturnsAsync((ImageDetails img) => img);

            this.repositoryMock
                .Setup(r => r.StreetcodeImageRepository
                .CreateAsync(It.IsAny<StreetcodeImage>()))
                .ReturnsAsync((StreetcodeImage si) => si);
        }

        public void SetupTagsRepositoryMocks()
        {
            this.repositoryMock
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

            this.repositoryMock
                .Setup(r => r.StreetcodeTagIndexRepository
                .CreateAsync(It.IsAny<StreetcodeTagIndex>()))
                .ReturnsAsync((StreetcodeTagIndex si) => si);
        }

        // CreateStreetcodeCommand Helpers
        public void VerifyStreetcodeCreatedSuccesfully(
            bool imagesIncluded = false,
            bool tagsIncluded = false,
            int times = 0)
        {
            this.repositoryMock.VerifySaveChangesAsyncCalledTimes(2);

            this.mapperMock
                .VerifyMapCalledOnce<StreetcodeContent>();

            this.mapperMock
                .VerifyMapCalledOnce<CreateStreetcodeDto>();

            this.streetcodeRepoMock
                .VerifyCreateAsyncCalledOnce<IStreetcodeRepository, StreetcodeContent>();

            if (imagesIncluded)
            {
                this.streetcodeImageRepoMock
                    .VerifyCreateAsyncCalledTimes
                    <IStreetcodeImageRepository, StreetcodeImage>(times);
                this.imageDetailsRepoMock
                    .VerifyCreateAsyncCalledTimes
                    <IImageDetailsRepository, ImageDetails>(times);
            }

            if (tagsIncluded)
            {
                this.streetcodeTagIndexRepoMock
                .VerifyCreateAsyncCalledTimes<IStreetcodeTagIndexRepository, StreetcodeTagIndex>(times);
            }
        }

        public void VerifyStreetcodeCreateFailed()
        {
            this.repositoryMock.VerifySaveChangesAsyncCalledOnce();
        }

        // DeleteStreetcodeCommand Helpers
        public void SetMocksForDelete()
        {
            this.repositoryMock
                .Setup(r => r.StreetcodeTagIndexRepository)
                .Returns(this.streetcodeTagIndexRepoMock.Object);

            this.repositoryMock
                .Setup(r => r.StreetcodeImageRepository)
                .Returns(this.streetcodeImageRepoMock.Object);

            this.repositoryMock
                .Setup(r => r.ImageDetailsRepository)
                .Returns(this.imageDetailsRepoMock.Object);

            this.streetcodeTagIndexRepoMock.SetupGetAllAsync(new List<StreetcodeTagIndex>()
            {
                new StreetcodeTagIndex { TagId = 15, StreetcodeId = 1 },
                new StreetcodeTagIndex { TagId = 20, StreetcodeId = 1 },
            });

            this.streetcodeImageRepoMock.SetupGetAllAsync(new List<StreetcodeImage>()
            {
                new StreetcodeImage { StreetcodeId = 1, ImageId = 10 },
                new StreetcodeImage { StreetcodeId = 1, ImageId = 15 },
            });

            this.imageDetailsRepoMock.SetupGetAllAsync(new List<ImageDetails>()
            {
                new ImageDetails { Id = 1, ImageId = 10 },
                new ImageDetails { Id = 2, ImageId = 15 },
            });

            this.streetcodeTagIndexRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<StreetcodeTagIndex>>()));

            this.streetcodeImageRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<StreetcodeImage>>()));

            this.imageDetailsRepoMock
                .Setup(r => r.DeleteRange(It.IsAny<IEnumerable<ImageDetails>>()));
        }

        public void VerifyDeleteSuccesful()
        {
            this.streetcodeTagIndexRepoMock
                .VerifyDeleteRangeCalledOnce<IStreetcodeTagIndexRepository, StreetcodeTagIndex>();
            this.streetcodeImageRepoMock
                .VerifyDeleteRangeCalledOnce<IStreetcodeImageRepository, StreetcodeImage>();
            this.imageDetailsRepoMock
                .VerifyDeleteRangeCalledOnce<IImageDetailsRepository, ImageDetails>();
            this.audioRepositoryMock
                .VerifyDeleteCalledOnce<IAudioRepository, Audio>();
            this.repositoryMock
                .VerifySaveChangesAsyncCalledOnce();
        }

        // UpdateStreetcodeCommand Helpers
        public void VerifyStreetcodeUpdatedSuccesfully(
            bool imagesIncluded = false,
            bool tagsIncluded = false,
            int timesImages = 0,
            int timesTags = 0)
        {
            this.repositoryMock.VerifySaveChangesAsyncCalledOnce();

            this.mapperMock
                .VerifyMapCalledOnce<UpdateStreetcodeDto>();

            this.streetcodeRepoMock
                .VerifyUpdateCalledOnce<IStreetcodeRepository, StreetcodeContent>();

            if (imagesIncluded)
            {
                this.streetcodeImageRepoMock
                    .VerifyDeleteRangeCalledOnce
                    <IStreetcodeImageRepository, StreetcodeImage>();
                this.imageDetailsRepoMock
                    .VerifyDeleteRangeCalledOnce
                    <IImageDetailsRepository, ImageDetails>();

                this.streetcodeImageRepoMock
                    .VerifyCreateAsyncCalledTimes
                    <IStreetcodeImageRepository, StreetcodeImage>(timesImages);
                this.imageDetailsRepoMock
                    .VerifyCreateAsyncCalledTimes
                    <IImageDetailsRepository, ImageDetails>(timesImages);
            }

            if (tagsIncluded)
            {
                this.streetcodeTagIndexRepoMock
                .VerifyDeleteRangeCalledOnce<IStreetcodeTagIndexRepository, StreetcodeTagIndex>();

                this.streetcodeTagIndexRepoMock
                .VerifyCreateAsyncCalledTimes<IStreetcodeTagIndexRepository, StreetcodeTagIndex>(timesTags);
            }
        }

        public UpdateStreetcodeCommand PrepareValidRequest(string json = null)
        {
            json ??= StreetcodeTestData.CreatePersonStreetcode();

            using var doc = JsonDocument.Parse(json);
            return new UpdateStreetcodeCommand(1, doc.RootElement.Clone());
        }

        public void SetupSuccessfulUpdate(UpdateStreetcodeCommand request)
        {
            var dto = new StreetcodeCreateHelper(loggerMock.Object)
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

            this.repositoryMock.SetupSaveChangesAsync();

            this.SetupStreetcodeExists(entity);
        }

        public void SetupStreetcodeExists(StreetcodeContent entity = null)
        {
            entity ??= new StreetcodeContent { Id = 1, Index = 1 };

            this.repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(this.streetcodeRepoMock.Object);

            this.streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(entity);

            this.streetcodeRepoMock
                .Setup(r => r.Update(It.IsAny<StreetcodeContent>()));
        }

        public void SetupStreetcodeNotFound()
        {
            this.repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(this.streetcodeRepoMock.Object);

            this.streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync((StreetcodeContent)null);
        }
    }
}