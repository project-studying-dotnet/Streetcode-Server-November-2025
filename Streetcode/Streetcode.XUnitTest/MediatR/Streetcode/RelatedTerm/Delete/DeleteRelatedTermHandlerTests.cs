using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.MediatR.RelatedTerm.Fixtures;
using Xunit;
using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Delete;

public class DeleteRelatedTermHandlerTests
{
    private const string ExistedWord = "ExistingWord";
    private const string NonExistentWord = "NonExistentWord";
    private const int ExistingTermId = 1;
    private const int NonExistentTermId = 999;

    private readonly Mock<IRepositoryWrapper> mockRepository;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ILoggerService> mockLogger;
    private readonly DeleteRelatedTermHandler handler;

    private readonly Entity existingRelatedTermEntity = new () { Id = 1, TermId = ExistingTermId, Word = ExistedWord };
    private readonly RelatedTermDto expectedRelatedTermDto = new () { Id = 1, TermId = ExistingTermId, Word = ExistedWord };

    public DeleteRelatedTermHandlerTests()
    {
        this.mockRepository = new Mock<IRepositoryWrapper>();
        this.mockMapper = new Mock<IMapper>();
        this.mockLogger = new Mock<ILoggerService>();
        this.handler = new DeleteRelatedTermHandler(this.mockRepository.Object, this.mockMapper.Object, this.mockLogger.Object);
        this.mockLogger.Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));
    }

    [Fact]
    public async Task Handle_ExistingWordAndTermId_ShouldReturnSuccess()
    {
        var command = new DeleteRelatedTermCommand(ExistedWord, ExistingTermId);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(this.existingRelatedTermEntity);

        this.mockRepository.Setup(r => r.RelatedTermRepository.Delete(It.IsAny<Entity>()));
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        this.mockMapper.Setup(m => m.Map<RelatedTermDto>(this.existingRelatedTermEntity))
            .Returns(this.expectedRelatedTermDto);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(this.expectedRelatedTermDto.Word, result.Value.Word);
        Assert.Equal(this.expectedRelatedTermDto.TermId, result.Value.TermId);

        this.mockRepository.VerifyGetFirstOrDefaultAsyncCalledOnce();
        this.mockRepository.VerifyDeleteCalledOnce(this.existingRelatedTermEntity);
        this.mockRepository.VerifySaveChangesCalledOnce();

        this.mockMapper.VerifyMapEntityToDtoCalledOnce();

        this.mockLogger.VerifyLogErrorCalledNever();
    }

    [Fact]
    public async Task Handle_NonExistentWord_ShouldReturnFailure()
    {
        var command = new DeleteRelatedTermCommand(NonExistentWord, ExistingTermId);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync((Entity)null!);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal($"Cannot find a related term: {NonExistentWord}", result.Errors.First().Message);

        this.mockRepository.VerifyGetFirstOrDefaultAsyncCalledOnce();

        this.mockRepository.VerifyDeleteCalledNever();
        this.mockRepository.VerifySaveChangesCalledNever();

        this.mockMapper.VerifyMapEntityToDtoCalledNever();

        this.mockLogger.VerifyLogErrorCalledOnce();
    }

    [Fact]
    public async Task Handle_NonExistentTermId_ShouldReturnFailure()
    {
        var command = new DeleteRelatedTermCommand(ExistedWord, NonExistentTermId);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync((Entity)null!);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal($"Cannot find a related term: {ExistedWord}", result.Errors.First().Message);

        this.mockRepository.VerifyGetFirstOrDefaultAsyncCalledOnce();

        this.mockRepository.VerifyDeleteCalledNever();
        this.mockRepository.VerifySaveChangesCalledNever();

        this.mockMapper.VerifyMapEntityToDtoCalledNever();

        this.mockLogger.VerifyLogErrorCalledOnce();
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ShouldReturnFailure()
    {
        var command = new DeleteRelatedTermCommand(ExistedWord, ExistingTermId);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(this.existingRelatedTermEntity);

        this.mockRepository.Setup(r => r.RelatedTermRepository.Delete(It.IsAny<Entity>()));
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

        this.mockMapper.Setup(m => m.Map<RelatedTermDto>(this.existingRelatedTermEntity))
            .Returns(this.expectedRelatedTermDto);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal("Failed to delete a related term", result.Errors.First().Message);

        this.mockRepository.VerifyGetFirstOrDefaultAsyncCalledOnce();

        this.mockRepository.VerifyDeleteCalledOnce(this.existingRelatedTermEntity);
        this.mockRepository.VerifySaveChangesCalledOnce();

        this.mockMapper.VerifyMapEntityToDtoCalledOnce();

        this.mockLogger.VerifyLogErrorCalledOnce();
    }

    [Fact]
    public async Task Handle_MappingToDtoFails_ShouldReturnFailure()
    {
        var command = new DeleteRelatedTermCommand(ExistedWord, ExistingTermId);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(this.existingRelatedTermEntity);

        this.mockRepository.Setup(r => r.RelatedTermRepository.Delete(It.IsAny<Entity>()));
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        this.mockMapper.Setup(m => m.Map<RelatedTermDto>(this.existingRelatedTermEntity))
            .Returns((RelatedTermDto)null!);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal("Failed to delete a related term", result.Errors.First().Message);

        this.mockRepository.VerifyGetFirstOrDefaultAsyncCalledOnce();

        this.mockRepository.VerifyDeleteCalledOnce(this.existingRelatedTermEntity);
        this.mockRepository.VerifySaveChangesCalledOnce();

        this.mockMapper.VerifyMapEntityToDtoCalledOnce();

        this.mockLogger.VerifyLogErrorCalledOnce();
    }
}