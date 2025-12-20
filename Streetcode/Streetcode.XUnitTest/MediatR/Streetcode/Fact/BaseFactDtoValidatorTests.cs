namespace Streetcode.XUnitTest.MediatR.Fact
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Streetcode.Fact;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class BaseFactDtoValidatorTests
    {
        private readonly TestFactDtoValidator _validator;

        public BaseFactDtoValidatorTests()
        {
            _validator = new TestFactDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Title_Is_Empty(string title)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = title;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactTitleRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Long()
        {
            var dto = CreateValidDto();
            dto.Title = new string('a', ValidationConstants.Fact.TitleMaxLength + 1);

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == string.Format(ErrorMessages.FactTitleTooLong, ValidationConstants.Fact.TitleMaxLength));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Content_Is_Empty(string content)
        {
            var dto = CreateValidDto();
            dto.Content = content;

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactContentRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Too_Long()
        {
            var dto = CreateValidDto();
            dto.Content = new string('a', ValidationConstants.Fact.ContentMaxLength + 1);

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == string.Format(ErrorMessages.FactContentTooLong, ValidationConstants.Fact.ContentMaxLength));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_ImageId_Is_Invalid(int imageId)
        {
            var dto = CreateValidDto();
            dto.ImageId = imageId;

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactImageIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            var dto = CreateValidDto();
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        private TestFactDto CreateValidDto()
        {
            return new TestFactDto
            {
                Title = "Valid Title",
                Content = "Valid Content",
                ImageId = 1,
            };
        }

        public class TestFactDto
        {
            public string Title { get; set; } = string.Empty;

            public string Content { get; set; } = string.Empty;

            public int ImageId { get; set; }
        }

        public class TestFactDtoValidator : BaseFactDtoValidator<TestFactDto>
        {
            public TestFactDtoValidator()
            {
                ConfigureSharedRules();
            }

            protected override string GetTitle(TestFactDto dto) => dto.Title;

            protected override string GetFactContent(TestFactDto dto) => dto.Content;

            protected override int GetImageId(TestFactDto dto) => dto.ImageId;
        }
    }
}