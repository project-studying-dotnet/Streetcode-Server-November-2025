using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.RelatedTerm.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Streetcode.BLL;
using Xunit;
using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Update;

public class UpdateRelatedTermHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;
    private readonly UpdateRelatedTermHandler _handler;

    public UpdateRelatedTermHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockRepository = new Mock<IRepositoryWrapper>();
        _mockLogger = new Mock<ILoggerService>();

        _handler = new UpdateRelatedTermHandler(_mockMapper.Object, _mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ExistingTerm_ReturnsSuccessResult()
    {
        var dto = new RelatedTermDto { Id = 1, Word = "Updated", TermId = 10 };
        var existingEntity = new Entity { Id = 1, Word = "Old", TermId = 10 };
        var command = new UpdateRelatedTermCommand(1, dto);

        _mockRepository
            .Setup(
                r => r.RelatedTermRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Entity, bool>>>(), null))
            .ReturnsAsync(existingEntity);

        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        _mockMapper.Setup(m => m.Map<RelatedTermDto>(existingEntity)).Returns(dto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(dto);

        _mockMapper.Verify(m => m.Map(dto, existingEntity), Times.Once);
        _mockRepository.Verify(r => r.RelatedTermRepository.Update(existingEntity), Times.Once);
    }

    [Fact]
    public async Task Handle_TermNotFound_ReturnsFailedResultWithLogger()
    {
        var dto = new RelatedTermDto { Id = 99 };
        var command = new UpdateRelatedTermCommand(99, dto);

        _mockRepository
            .Setup(
                r => r.RelatedTermRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Entity, bool>>>(), null))
            .ReturnsAsync((Entity?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be(ErrorMessages.RelatedTermNotFound);

        _mockLogger.Verify(l => l.LogError(command, ErrorMessages.RelatedTermNotFound), Times.Once);
    }

    [Fact]
    public async Task Handle_SaveChangesAsyncReturnsZero_ReturnsFailedResult()
    {
        var dto = new RelatedTermDto { Id = 1 };
        var existingEntity = new Entity { Id = 1 };
        var command = new UpdateRelatedTermCommand(1, dto);

        _mockRepository
            .Setup(
                r => r.RelatedTermRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Entity, bool>>>(), null))
            .ReturnsAsync(existingEntity);

        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Помилка при оновленні пов'язаного терміну");
    }

    [Fact]
    public async Task Handle_MapperReturnsNull_ReturnsFailedResult()
    {
        var dto = new RelatedTermDto { Id = 1 };
        var existingEntity = new Entity { Id = 1 };
        var command = new UpdateRelatedTermCommand(1, dto);

        _mockRepository
            .Setup(
                r => r.RelatedTermRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Entity, bool>>>(), null))
            .ReturnsAsync(existingEntity);

        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        _mockMapper.Setup(m => m.Map<RelatedTermDto>(existingEntity)).Returns((RelatedTermDto?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Помилка при оновленні пов'язаного терміну");
    }
}