namespace Streetcode.XUnitTest.MediatR.Fact.Update
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Update;
    using Xunit;

    public class UpdateFactDtoValidatorTests
    {
        private readonly UpdateFactDtoValidator _validator;

        public UpdateFactDtoValidatorTests()
        {
            _validator = new UpdateFactDtoValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Id_Is_Invalid(int invalidId)
        {
            // Arrange
            var dto = new UpdateFactDto { Id = invalidId };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.FactIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            // Arrange
            var dto = new UpdateFactDto { Id = 1 };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Be_Valid_When_All_Fields_Are_Correct()
        {
            // Arrange
            var dto = new UpdateFactDto
            {
                Id = 10,
                Title = "Valid Title",
                FactContent = "Some valid content description",
                ImageId = 5,
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}