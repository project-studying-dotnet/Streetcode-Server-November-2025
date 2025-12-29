namespace Streetcode.XUnitTest.MediatR.Newss.Update
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.News;
 using global::Streetcode.BLL.MediatR.Newss.Update;
    using Xunit;

    public class UpdateNewsCommandValidatorTests
    {
        private readonly UpdateNewsCommandValidator _validator;

        public UpdateNewsCommandValidatorTests()
        {
            _validator = new UpdateNewsCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_News_Is_Null()
        {
            var command = new UpdateNewsCommand(null);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.news);
        }

        [Fact]
        public void Should_Have_Error_When_News_Id_Is_Invalid()
        {
            // Arrange
            var dto = new NewsDto
            {
                Id = 0, // Невалідний ID для Update
                Title = "Valid Title",
                Text = "Valid Text",
                URL = "Valid URL",
                CreationDate = System.DateTime.UtcNow,
            };
            var command = new UpdateNewsCommand(dto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.news.Id)
                  .WithErrorMessage(ErrorMessages.NewsIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            var dto = new NewsDto
            {
                Id = 1,
                Title = string.Empty,
            };
            var command = new UpdateNewsCommand(dto);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.news.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var dto = new NewsDto
            {
                Id = 1,
                Title = "Valid Title",
                Text = "Valid Text",
                URL = "Valid URL",
                CreationDate = System.DateTime.UtcNow.AddMinutes(-1),
            };
            var command = new UpdateNewsCommand(dto);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}