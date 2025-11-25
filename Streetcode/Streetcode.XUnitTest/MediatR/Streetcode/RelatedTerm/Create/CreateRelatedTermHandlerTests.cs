using System.Linq.Expressions;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;
using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.XUnitTest.MediatR.Streetcode.RelatedTerm.Create;

public class CreateRelatedTermHandlerTests
{
    private readonly Mock<IRepositoryWrapper> mockRepository;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<ILoggerService> mockLogger;
    private readonly CreateRelatedTermHandler handler;

    private readonly RelatedTermDTO validRelatedTermDto = new() { Id = 1, TermId = 1, Word = "Тест" };
    private readonly Entity validRelatedTermEntity = new() { Id = 1, TermId = 1, Word = "Тест" };

    private readonly RelatedTermDTO createdRelatedTermDto = new() { Id = 1, TermId = 1, Word = "Тест" };

    public CreateRelatedTermHandlerTests()
    {
        this.mockRepository = new Mock<IRepositoryWrapper>();
        this.mockMapper = new Mock<IMapper>();
        this.mockLogger = new Mock<ILoggerService>();
        this.handler = new CreateRelatedTermHandler(this.mockRepository.Object, this.mockMapper.Object, this.mockLogger.Object);

        this.mockRepository.Setup(r => r.RelatedTermRepository.Create(It.IsAny<Entity>()))
            .Returns(this.validRelatedTermEntity);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .Returns(Task.FromResult(Enumerable.Empty<Entity>()));

        this.mockLogger.Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));
    }

    [Fact]
    public async Task Handle_ValidData_ShouldReturnSuccess()
    {
        var command = new CreateRelatedTermCommand(this.validRelatedTermDto);
        
        this.mockMapper.Setup(m => m.Map<Entity>(this.validRelatedTermDto)).Returns(this.validRelatedTermEntity);
        this.mockMapper.Setup(m => m.Map<RelatedTermDTO>(this.validRelatedTermEntity))
            .Returns(this.createdRelatedTermDto);
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);

        Assert.Equal(this.createdRelatedTermDto.Word, result.Value.Word);
        
        this.mockRepository.Verify(r => r.RelatedTermRepository.Create(It.IsAny<Entity>()), Times.Once);
        this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateExists_ShouldReturnFailure()
    {
        var command = new CreateRelatedTermCommand(this.validRelatedTermDto);

        this.mockRepository.Setup(r => r.RelatedTermRepository.GetAllAsync(
                It.IsAny<Expression<Func<Entity, bool>>>(),
                It.IsAny<Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>>()))
            .ReturnsAsync(new List<Entity> { this.validRelatedTermEntity });

        this.mockMapper.Setup(m => m.Map<Entity>(this.validRelatedTermDto)).Returns(this.validRelatedTermEntity);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal("Слово з цим визначенням уже існує", result.Errors.First().Message);

        this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ShouldReturnFailure()
    {
        var command = new CreateRelatedTermCommand(this.validRelatedTermDto);

        this.mockMapper.Setup(m => m.Map<Entity>(this.validRelatedTermDto)).Returns(this.validRelatedTermEntity);
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);
        Assert.Equal("Cannot save changes in the database after related word creation!", result.Errors.First().Message);

        this.mockRepository.Verify(r => r.RelatedTermRepository.Create(It.IsAny<Entity>()), Times.Once);
        this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_MappingDtoToEntityFails_ShouldReturnFailure()
    {
        var command = new CreateRelatedTermCommand(this.validRelatedTermDto);

        this.mockMapper.Setup(m => m.Map<Entity>(this.validRelatedTermDto)).Returns((Entity)null!);

        var result = await this.handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailed);

        Assert.Equal("Cannot create new related word for a term!", result.Errors.First().Message);

        this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }
}