namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Update
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentValidation.TestHelper;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;
    using global::Streetcode.DAL.Entities.Streetcode;
    using global::Streetcode.DAL.Entities.Timeline;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using global::Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures;
    using global::Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using global::Streetcode.XUnitTest.MediatR.Streetcode.Fixture;
    using global::Streetcode.XUnitTest.Helpers;
    using Xunit;

    public class UpdateTimelineItemHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<ITimelineRepository> timelineRepositoryMock;
        private readonly Mock<IStreetcodeRepository> streetcodeRepositoryMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly Mock<IHistoricalContextTimelineRepository> historicalContextTimelineRepositoryMock;
        private readonly UpdateTimelineItemHandler handler;

        public UpdateTimelineItemHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.timelineRepositoryMock = new Mock<ITimelineRepository>();
            this.streetcodeRepositoryMock = new Mock<IStreetcodeRepository>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();
            this.historicalContextTimelineRepositoryMock = new Mock<IHistoricalContextTimelineRepository>();

            this.repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(this.timelineRepositoryMock.Object);

            this.repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(this.streetcodeRepositoryMock.Object);

            this.repositoryWrapperMock
                .Setup(rw => rw.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.repositoryWrapperMock
                .Setup(rw => rw.HistoricalContextTimelineRepository)
                .Returns(this.historicalContextTimelineRepositoryMock.Object);

            this.handler = new UpdateTimelineItemHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidDataAndNoContexts_ShouldUpdateTimelineItemSuccessfully()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var updatedItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(updatedItem);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(resultDto, result.Value);
            this.timelineRepositoryMock.Verify(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Once);
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidHistoricalContexts_ShouldUpdateTimelineItemWithAssociations()
        {
            // Arrange
            var contextIds = new List<int> { 1, 2, 3 };
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, historicalContextIds: contextIds);
            var existingItem = TimelineItemTestData.CreateTimelineItemWithContexts(id: 1, 10, 20);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var contexts = HistoricalContextTestData.CreateHistoricalContexts(3);
            var updatedItem = TimelineItemTestData.CreateTimelineItemWithContexts(id: 1, contextIds.ToArray());
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.historicalContextRepositoryMock.SetupGetAllAsync(contexts.AsQueryable());

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.historicalContextTimelineRepositoryMock
                .Setup(r => r.Delete(It.IsAny<HistoricalContextTimeline>()));

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(updatedItem);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<HistoricalContext, bool>>>(),
                    null),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldDeleteOldHistoricalContextRelationships()
        {
            // Arrange
            var newContextIds = new List<int> { 5, 6 };
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, historicalContextIds: newContextIds);
            var oldRelationships = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { TimelineId = 1, HistoricalContextId = 10 },
                new HistoricalContextTimeline { TimelineId = 1, HistoricalContextId = 20 },
            };
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            existingItem.HistoricalContextTimelines = oldRelationships;
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var contexts = new List<HistoricalContext>
            {
                HistoricalContextTestData.CreateHistoricalContext(id: 5, title: "Context 5"),
                HistoricalContextTestData.CreateHistoricalContext(id: 6, title: "Context 6")
            };
            var updatedItem = TimelineItemTestData.CreateTimelineItemWithContexts(id: 1, 5, 6);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ReturnsAsync(existingItem)
                .ReturnsAsync(updatedItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.historicalContextRepositoryMock.SetupGetAllAsync(contexts.AsQueryable());

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.historicalContextTimelineRepositoryMock
                .Setup(r => r.Delete(It.IsAny<HistoricalContextTimeline>()));

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.historicalContextTimelineRepositoryMock.Verify(
                r => r.Delete(It.IsAny<HistoricalContextTimeline>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_WithNonExistentTimelineItem_ShouldReturnFailure()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 999);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("999", result.Errors[0].Message);
            this.timelineRepositoryMock.Verify(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Never);
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_WithNonExistentStreetcode_ShouldReturnFailure()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, streetcodeId: 999);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(null);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Streetcode", result.Errors[0].Message);
            this.timelineRepositoryMock.Verify(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Never);
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_WithNonExistentHistoricalContextIds_ShouldReturnFailure()
        {
            // Arrange
            var invalidContextIds = new List<int> { 999, 1000 };
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, historicalContextIds: invalidContextIds);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var emptyContexts = new List<HistoricalContext>();

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.historicalContextRepositoryMock.SetupGetAllAsync(emptyContexts.AsQueryable());

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Historical contexts with IDs", result.Errors[0].Message);
            Assert.Contains("do not exist", result.Errors[0].Message);
            this.timelineRepositoryMock.Verify(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WithPartiallyValidHistoricalContextIds_ShouldReturnFailure()
        {
            // Arrange
            var requestedIds = new List<int> { 1, 2, 999 };
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, historicalContextIds: requestedIds);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();

            var existingContexts = new List<HistoricalContext>
            {
                new HistoricalContext { Id = 1, Title = "Context 1" },
                new HistoricalContext { Id = 2, Title = "Context 2" },
            };

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.historicalContextRepositoryMock.SetupGetAllAsync(existingContexts.AsQueryable());

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("999", result.Errors[0].Message);
            this.timelineRepositoryMock.Verify(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUseMapperForDtoToEntityMapping()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var updatedItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ReturnsAsync(existingItem)
                .ReturnsAsync(updatedItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.mapperMock.Verify(m => m.Map(updateDto, existingItem), Times.Once);
            this.mapperMock.Verify(m => m.Map<TimelineItemDto>(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()), Times.Once);
        }

        [Fact]
        public async Task Handle_AfterUpdate_ShouldRetrieveItemWithIncludes()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var updatedItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(updatedItem);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.timelineRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>,
                        IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_WithEmptyHistoricalContextIds_ShouldClearAllRelationships()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1, historicalContextIds: new List<int>());
            var oldRelationships = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { TimelineId = 1, HistoricalContextId = 10 },
            };
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            existingItem.HistoricalContextTimelines = oldRelationships;
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var updatedItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ReturnsAsync(existingItem)
                .ReturnsAsync(updatedItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.historicalContextTimelineRepositoryMock
                .Setup(r => r.Delete(It.IsAny<HistoricalContextTimeline>()));

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.historicalContextTimelineRepositoryMock.Verify(
                r => r.Delete(It.IsAny<HistoricalContextTimeline>()),
                Times.Once);
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<HistoricalContext, bool>>>(),
                    null),
                Times.Never);
        }

        [Fact]
        public async Task Handle_OnSaveChanges_ShouldCallSaveChangesExactlyOnce()
        {
            // Arrange
            var updateDto = TimelineItemTestData.CreateTimelineItemUpdateDto(id: 1);
            var existingItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var streetcode = StreetcodeTestData.CreateStreetcode();
            var updatedItem = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(existingItem);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingItem))
                .Returns(existingItem);

            this.timelineRepositoryMock.Setup(r => r.Update(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()));

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock.SetupGetFirstOrDefaultAsync(updatedItem);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(updatedItem))
                .Returns(resultDto);

            var command = new UpdateTimelineItemCommand(updateDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);
        }
    }
}
