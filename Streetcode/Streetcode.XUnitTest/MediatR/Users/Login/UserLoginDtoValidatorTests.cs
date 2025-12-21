namespace Streetcode.XUnitTest.MediatR.Users.Login
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Users;
    using Streetcode.BLL.MediatR.Users.Login;
    using Xunit;

    public class UserLoginDtoValidatorTests
    {
        private readonly UserLoginDtoValidator validator = new UserLoginDtoValidator();

        [Theory]
        [InlineData("Password123@", true)]
        [InlineData("", false)]
        public void Validator_ShouldValidatePassword(string password, bool isValid)
        {
            // Arrange
            var dto = new UserLoginDto
            {
                Email = "john.doe@gmail.com",
                Password = password,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Password);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.Password);
            }
        }
    }
}
