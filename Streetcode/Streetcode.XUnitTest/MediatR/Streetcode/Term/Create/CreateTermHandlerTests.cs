using AutoMapper;
using Moq;
using global::Streetcode.BLL.DTO.TextContent;
using global::Streetcode.BLL.Interfaces.Logging;
using global::Streetcode.BLL.MediatR.Term.Create;
using global::Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;
using FluentAssertions;
using TermEntity = global::Streetcode.DAL.Entities.Streetcode.TextContent.Term;

namespace Streetcode.XUnitTest.MediatR.Streetcode.Term.Create;

public class CreateTermHandlerTests
{
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<IRepositoryWrapper> mockRepository;
    private readonly Mock<ILoggerService> mockLogger;
    private readonly CreateTermHandler handler;

    public CreateTermHandlerTests()
    {
        this.mockMapper = new Mock<IMapper>();
        this.mockRepository = new Mock<IRepositoryWrapper>();
        this.mockLogger = new Mock<ILoggerService>();

        this.handler =
            new CreateTermHandler(this.mockMapper.Object, this.mockRepository.Object, this.mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccessAndDto()
    {
        var termDto = new TermDto { Title = "Test Title" };
        var termEntity = new TermEntity { Id = 1, Title = "Test Title" };
        var command = new CreateTermCommand(termDto);

        this.mockMapper.Setup(m => m.Map<TermEntity>(It.IsAny<TermDto>())).Returns(termEntity);

        this.mockRepository.Setup(r => r.TermRepository.CreateAsync(It.IsAny<TermEntity>()))
            .Returns(Task.FromResult(new TermEntity()));

        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        this.mockMapper.Setup(m => m.Map<TermDto>(It.IsAny<TermEntity>())).Returns(termDto);

        var result = await this.handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(termDto);

        this.mockRepository.Verify(x => x.TermRepository.CreateAsync(It.IsAny<TermEntity>()), Times.Once);
        this.mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_SaveFails_ReturnsFailedResult()
    {
        var termDto = new TermDto { Title = "Fail Title" };
        var termEntity = new TermEntity { Title = "Fail Title" };
        var command = new CreateTermCommand(termDto);

        this.mockMapper.Setup(m => m.Map<TermEntity>(It.IsAny<TermDto>())).Returns(termEntity);
        this.mockRepository.Setup(r => r.TermRepository.CreateAsync(It.IsAny<TermEntity>()))
            .Returns(Task.FromResult(termEntity));
        this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0);

        var result = await this.handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("Помилка при збереженні терміну");
    }

    [Fact]
    public async Task Handle_MapperThrowsException_HandlerPropagatesException()
    {
        var command = new CreateTermCommand(new TermDto());

        this.mockMapper.Setup(m => m.Map<TermEntity>(It.IsAny<TermDto>())).Throws(new Exception("Mapping error"));

        Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Mapping error");
    }
}