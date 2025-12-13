namespace Streetcode.XUnitTest.MediatR.Newss.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.MediatR.Newss.Create;
    using Xunit;

    public class CreateNewsCommandValidatorTests
    {
        private readonly CreateNewsCommandValidator _validator;

        public CreateNewsCommandValidatorTests()
        {
            _validator = new CreateNewsCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_News_Is_Null()
        {
            var command = new CreateNewsCommand(null);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.newNews);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            var invalidDto = new NewsDto { Title = string.Empty };
            var command = new CreateNewsCommand(invalidDto);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.newNews.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_News_Is_Valid()
        {
            // Arrange
            var validDto = new NewsDto
            {
                Title = "Title",
                Text = "Text",
                URL = "url",
                CreationDate = System.DateTime.UtcNow.AddMinutes(-1),
            };
            var command = new CreateNewsCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}