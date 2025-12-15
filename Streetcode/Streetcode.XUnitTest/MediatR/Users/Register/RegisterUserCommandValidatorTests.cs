namespace Streetcode.XUnitTest.MediatR.Users.Register
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Users;
    using Streetcode.BLL.MediatR.Users.Register;
    using Streetcode.DAL.Enums;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="RegisterUserCommandValidator"/>.
    /// Covers validation rules for <see cref="RegisterUserCommand"/> to ensure
    /// that the newUser property is not null and is properly populated.
    /// </summary>
    public class RegisterUserCommandValidatorTests
    {
        private readonly RegisterUserCommandValidator validator = new RegisterUserCommandValidator();

        /// <summary>
        /// Tests that validation fails when the newUser property is null.
        /// Ensures that a validation error is returned for the newUser field.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_NewUserIsNull()
        {
            // Arrange
            var command = new RegisterUserCommand(null!);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.newUser);
        }

        /// <summary>
        /// Tests that validation succeeds when the newUser property is properly populated.
        /// Ensures that no validation error is returned when a valid <see cref="RegisterUserDto"/> is provided.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_NewUserIsPopulated()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Name = "Test",
                UserName = "test",
                Email = "e@e.e",
                Password = "Aa123456",
                Role = UserRole.Administrator,
            };
            var command = new RegisterUserCommand(dto);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.newUser);
        }
    }
}