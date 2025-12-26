namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Update
{
    using global::Streetcode.BLL;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;
    using global::Streetcode.DAL.Enums;
    using FluentValidation.TestHelper;
    using Xunit;

    public class UpdateTimelineItemDtoValidatorTests
    {
        private readonly UpdateTimelineItemDtoValidator validator;

        public UpdateTimelineItemDtoValidatorTests()
        {
            this.validator = new UpdateTimelineItemDtoValidator();
        }

        private static UpdateTimelineItemDto CreateValidDto() =>
            new UpdateTimelineItemDto
            {
                Id = 1,
                Title = "Valid Title",
                Description = "Valid description content",
                Date = new DateTime(2024, 12, 26),
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = 1,
                HistoricalContextIds = new List<int> { 1, 2 },
            };

        [Fact]
        public void Should_Pass_Validation_When_All_Fields_Are_Valid()
        {
            // Arrange
            var dto = CreateValidDto();

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Pass_Validation_When_HistoricalContextIds_Is_Empty()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.HistoricalContextIds = new List<int>();

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Id = 0;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.TimelineItemIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Id = -1;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.TimelineItemIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Pass_Validation_When_Id_Is_Positive()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Id = 42;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Null()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = null!;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage(ErrorMessages.TimelineItemTitleRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = string.Empty;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage(ErrorMessages.TimelineItemTitleRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Whitespace()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = "   ";

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage(ErrorMessages.TimelineItemTitleRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_MaxLength()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = new string('A', 29); // 29 characters, exceeds max of 28

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage(string.Format(ErrorMessages.TimelineItemTitleTooLong, 28));
        }

        [Fact]
        public void Should_Pass_Validation_When_Title_Is_At_MaxLength()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = new string('A', 28); // Exactly 28 characters

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Null()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Description = null!;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage(ErrorMessages.TimelineItemDescriptionRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Description = string.Empty;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage(ErrorMessages.TimelineItemDescriptionRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Whitespace()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Description = "   ";

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage(ErrorMessages.TimelineItemDescriptionRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_MaxLength()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Description = new string('A', 401); // 401 characters, exceeds max of 400

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage(string.Format(ErrorMessages.TimelineItemDescriptionTooLong, 400));
        }

        [Fact]
        public void Should_Pass_Validation_When_Description_Is_At_MaxLength()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Description = new string('A', 400); // Exactly 400 characters

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_Default()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Date = default(DateTime);

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Date)
                  .WithErrorMessage(ErrorMessages.TimelineItemDateRequired);
        }

        [Fact]
        public void Should_Pass_Validation_When_Date_Is_Valid()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Date = new DateTime(2023, 5, 15);

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
        }

        [Fact]
        public void Should_Have_Error_When_DateViewPattern_Is_Invalid()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.DateViewPattern = (DateViewPattern)999; // Invalid enum value

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DateViewPattern)
                  .WithErrorMessage(ErrorMessages.TimelineItemDateViewPatternInvalid);
        }

        [Theory]
        [InlineData(DateViewPattern.DateMonthYear)]
        [InlineData(DateViewPattern.MonthYear)]
        [InlineData(DateViewPattern.SeasonYear)]
        [InlineData(DateViewPattern.Year)]
        public void Should_Pass_Validation_When_DateViewPattern_Is_Valid(DateViewPattern pattern)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.DateViewPattern = pattern;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DateViewPattern);
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeId_Is_Zero()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.StreetcodeId = 0;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage(ErrorMessages.TimelineItemStreetcodeIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeId_Is_Negative()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.StreetcodeId = -1;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage(ErrorMessages.TimelineItemStreetcodeIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Pass_Validation_When_StreetcodeId_Is_Positive()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.StreetcodeId = 42;

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeId);
        }

        [Fact]
        public void Should_Have_Error_When_HistoricalContextId_Is_Zero()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.HistoricalContextIds = new List<int> { 1, 0, 3 };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[1]")
                  .WithErrorMessage(ErrorMessages.TimelineItemHistoricalContextIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_HistoricalContextId_Is_Negative()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.HistoricalContextIds = new List<int> { 1, -5, 3 };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[1]")
                  .WithErrorMessage(ErrorMessages.TimelineItemHistoricalContextIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Multiple_Errors_When_Multiple_HistoricalContextIds_Are_Invalid()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.HistoricalContextIds = new List<int> { 0, -1, -5 };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            Assert.Equal(3, result.Errors.Count);
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[0]");
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[1]");
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[2]");
        }

        [Fact]
        public void Should_Pass_Validation_When_All_HistoricalContextIds_Are_Positive()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.HistoricalContextIds = new List<int> { 1, 2, 3, 100 };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_Multiple_Errors_When_Multiple_Fields_Are_Invalid()
        {
            // Arrange
            var dto = new UpdateTimelineItemDto
            {
                Id = 0,
                Title = null!,
                Description = string.Empty,
                Date = default(DateTime),
                DateViewPattern = (DateViewPattern)999,
                StreetcodeId = 0,
                HistoricalContextIds = new List<int> { 0, -1 },
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            Assert.True(result.Errors.Count >= 7); // At least 7 errors expected
            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.Title);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Date);
            result.ShouldHaveValidationErrorFor(x => x.DateViewPattern);
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId);
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[0]");
            result.ShouldHaveValidationErrorFor("HistoricalContextIds[1]");
        }
    }
}
