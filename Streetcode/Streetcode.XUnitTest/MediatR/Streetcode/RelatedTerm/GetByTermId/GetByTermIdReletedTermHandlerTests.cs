using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using global::Streetcode.BLL;
using global::Streetcode.BLL.DTO.TextContent;
using global::Streetcode.BLL.Interfaces.Logging;
using global::Streetcode.BLL.MediatR.RelatedTerm.GetAllByTermId;
using global::Streetcode.DAL.Repositories.Interfaces.Base;
using global::Streetcode.XUnitTest.MediatR.RelatedTerm.Fixtures;
using Xunit;
using Entity = global::Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.XUnitTest.MediatR.RelatedTerm.GetByTermId;

public class GetByTermIdRelatedTermsHandlerTests
{
    private readonly Mock<IRepositoryWrapper> mockRepository;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ILoggerService> mockLogger;
    private readonly GetAllRelatedTermsByTermIdHandler handler;

    private const int VALID_TERM_ID = 1;
    private const int INVALID_TERM_ID = 100;
    
    private readonly List<Entity> relatedTermsEntities = new ()
    {
        new Entity { Id = 1, TermId = VALID_TERM_ID, Word = "Word1" },
        new Entity { Id = 2, TermId = VALID_TERM_ID, Word = "Word2" },
    };

    private readonly List<RelatedTermDto> relatedTermsDtos = new ()
    {
        new RelatedTermDto { Id = 1, TermId = VALID_TERM_ID, Word = "Word1" },
        new RelatedTermDto { Id = 2, TermId = VALID_TERM_ID, Word = "Word2" },
    };

    public GetByTermIdRelatedTermsHandlerTests()
    {
        this.mockRepository = new Mock<IRepositoryWrapper>();
        this.mockMapper = new Mock<IMapper>();
        this.mockLogger = new Mock<ILoggerService>();
        
        this.handler = new GetAllRelatedTermsByTermIdHandler(
            this.mockMapper.Object,
            this.mockRepository.Object,
            this.mockLogger.Object);

        this.mockLogger.Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));
    }

    [Fact]
    public async Task Handle_ExistingTermId_ShouldReturnSuccess()
    {
        var query = new GetAllRelatedTermsByTermIdQuery(VALID_TERM_ID);
        
        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(this.relatedTermsEntities);
        
        this.mockMapper.Setup(m => m.Map<IEnumerable<RelatedTermDto>>(It.IsAny<IEnumerable<Entity>>())) 
            .Returns(this.relatedTermsDtos);
        
        var result = await this.handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(this.relatedTermsDtos.Count, result.Value.Count());
        
        this.mockRepository.VerifyGetAllAsyncCalledOnce();
        this.mockMapper.VerifyMapEntityListToDtoListCalledOnce();
        this.mockLogger.VerifyLogErrorCalledNever();
    }

    [Fact]
    public async Task Handle_RepositoryReturnsNull_ShouldReturnFailure()
    {
        var query = new GetAllRelatedTermsByTermIdQuery(VALID_TERM_ID);
        
        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync((List<Entity>)null!);
        
        var result = await this.handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsFailed);
        Assert.Equal(string.Format(ErrorMessages.RelatedTermsNotFoundByTermId, VALID_TERM_ID), result.Errors.First().Message);
        
        this.mockRepository.VerifyGetAllAsyncCalledOnce();
        
        this.mockMapper.Verify(m => m.Map<IEnumerable<RelatedTermDto>>(It.IsAny<IEnumerable<Entity>>()), Times.Never);
        
        this.mockLogger.VerifyLogErrorCalledOnce();
    }
    
    [Fact]
    public async Task Handle_NotFoundTermId_ShouldReturnEmptyListSuccess()
    {
        var query = new GetAllRelatedTermsByTermIdQuery(INVALID_TERM_ID);
        
        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(Enumerable.Empty<Entity>());
        
        this.mockMapper.Setup(m => m.Map<IEnumerable<RelatedTermDto>>(It.Is<IEnumerable<Entity>>(e => !e.Any())))
            .Returns(Enumerable.Empty<RelatedTermDto>());
        
        var result = await this.handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
        
        this.mockRepository.VerifyGetAllAsyncCalledOnce();
        this.mockMapper.VerifyMapEntityListToDtoListCalledOnce(); 
        this.mockLogger.VerifyLogErrorCalledNever();
    }

    [Fact]
    public async Task Handle_MappingToDtoFails_ShouldReturnFailure()
    {
        var query = new GetAllRelatedTermsByTermIdQuery(VALID_TERM_ID);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(this.relatedTermsEntities);
        
        this.mockMapper.Setup(m => m.Map<IEnumerable<RelatedTermDto>>(It.IsAny<IEnumerable<Entity>>()))
            .Returns((IEnumerable<RelatedTermDto>)null!);
        
        var result = await this.handler.Handle(query, CancellationToken.None);
        
        Assert.True(result.IsFailed);
        Assert.Equal(ErrorMessages.RelatedTermsMappingFailed, result.Errors.First().Message);
        
        this.mockRepository.VerifyGetAllAsyncCalledOnce();
        this.mockMapper.VerifyMapEntityListToDtoListCalledOnce(); 
        this.mockLogger.VerifyLogErrorCalledOnce();
    }
}