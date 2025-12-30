using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Fixtures
{
    public static class RelatedTermMockExtensions
    {
        public static Mock<IRepositoryWrapper> VerifyGetAllAsyncCalledOnce(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<System.Func<Entity, bool>>>(),
                It.IsAny<System.Func<System.Linq.IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()),
                Times.Once);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyGetAllAsyncCalledNever(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<System.Func<Entity, bool>>>(),
                It.IsAny<System.Func<System.Linq.IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()),
                Times.Never);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyCreateCalledOnce(this Mock<IRepositoryWrapper> mock, Entity expectedEntity)
        {
            mock.Verify(x => x.RelatedTermRepository.CreateAsync(It.IsAny<Entity>()), Times.Once);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyCreateCalledNever(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.RelatedTermRepository.CreateAsync(It.IsAny<Entity>()), Times.Never);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifySaveChangesCalledOnce(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.SaveChangesAsync(), Times.Once);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifySaveChangesCalledNever(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.SaveChangesAsync(), Times.Never);
            return mock;
        }

        public static Mock<IMapper> VerifyMapDtoToEntityCalledOnce(this Mock<IMapper> mock)
        {
            mock.Verify(m => m.Map<Entity>(It.IsAny<RelatedTermDto>()), Times.Once);
            return mock;
        }

        public static Mock<IMapper> VerifyMapEntityToDtoCalledOnce(this Mock<IMapper> mock)
        {
            mock.Verify(m => m.Map<RelatedTermDto>(It.IsAny<Entity>()), Times.Once);
            return mock;
        }

        public static Mock<IMapper> VerifyMapEntityToDtoCalledNever(this Mock<IMapper> mock)
        {
            mock.Verify(m => m.Map<RelatedTermDto>(It.IsAny<Entity>()), Times.Never);
            return mock;
        }

        public static Mock<ILoggerService> VerifyLogErrorCalledOnce(this Mock<ILoggerService> mock)
        {
            mock.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
            return mock;
        }

        public static Mock<ILoggerService> VerifyLogErrorCalledNever(this Mock<ILoggerService> mock)
        {
            mock.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Never);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyGetFirstOrDefaultAsyncCalledOnce(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.RelatedTermRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<System.Func<Entity, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()),
                Times.Once);
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyDeleteCalledOnce(this Mock<IRepositoryWrapper> mock, Entity expectedEntity)
        {
            mock.Verify(x => x.RelatedTermRepository.Delete(expectedEntity), Times.Once); 
            return mock;
        }

        public static Mock<IRepositoryWrapper> VerifyDeleteCalledNever(this Mock<IRepositoryWrapper> mock)
        {
            mock.Verify(x => x.RelatedTermRepository.Delete(It.IsAny<Entity>()), Times.Never);
            return mock;
        }

        public static Mock<IMapper> VerifyMapEntityListToDtoListCalledOnce(this Mock<IMapper> mock)
        {
            mock.Verify(m => m.Map<IEnumerable<RelatedTermDto>>(It.IsAny<IEnumerable<Entity>>()), Times.Once);
            return mock;
        }
    }
}