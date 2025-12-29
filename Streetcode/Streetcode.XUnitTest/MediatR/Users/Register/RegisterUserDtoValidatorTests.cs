namespace Streetcode.XUnitTest.MediatR.Users.Register
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Users;
 using global::Streetcode.BLL.MediatR.Users.Register;
 using global::Streetcode.DAL.Enums;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="RegisterUserDtoValidator"/>.
    /// Covers validation rules for user registration data including password format,
    /// email format, username constraints, name requirements, phone number format, and role validation.
    /// </summary>
    public class RegisterUserDtoValidatorTests
    {
        private readonly RegisterUserDtoValidator validator = new RegisterUserDtoValidator();

        /// <summary>
        /// Tests that the password field validates correctly based on format requirements.
        /// Password must contain at least 6 characters, including at least one uppercase letter and one digit.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("Password1A", true)]
        [InlineData("passA1", true)]
        [InlineData("sht1A", false)]
        [InlineData("Onlylowercase1", true)]
        [InlineData("ONLYUPPERCASE1", false)]
        [InlineData("noupperlower1", false)]
        [InlineData("ValidPassword123A", true)]
        public void Password_Should_Validate_Format(string password, bool isValid)
        {
            var dto = new RegisterUserDto { Password = password, Name = "A", UserName = "TestUser", Email = "t@t.t", Role = UserRole.Administrator };
            var result = this.validator.TestValidate(dto);
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Password);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.Password);
            }
        }

        /// <summary>
        /// Tests that the email field validates correctly based on standard email format.
        /// Email must not be empty and must follow a valid email address pattern.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("validemail@test.com", true)]
        [InlineData("invalidemail", false)]
        [InlineData("", false)]
        public void Email_Should_Validate_Format(string email, bool isValid)
        {
            var dto = new RegisterUserDto { Email = email, Password = "Aa123456", Name = "A", UserName = "user", Role = UserRole.Administrator };
            var result = this.validator.TestValidate(dto);
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Email);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.Email);
            }
        }

        /// <summary>
        /// Tests that the username field validates correctly based on length constraints.
        /// Username must not be empty and must not exceed the maximum allowed length.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("ValidUser", true)]
        [InlineData("", false)]
        [InlineData("ThisUserNameIsWayTooLongForTheMaxLen", false)]
        public void UserName_Should_Validate(string username, bool isValid)
        {
            var dto = new RegisterUserDto { UserName = username, Password = "Aa123456", Name = "A", Email = "m@m.m", Role = UserRole.Administrator };
            var result = this.validator.TestValidate(dto);
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.UserName);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.UserName);
            }
        }

        /// <summary>
        /// Tests that the name field validates correctly based on presence and length requirements.
        /// Name must not be empty or whitespace and must not exceed the maximum allowed length.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ValidName")]
        public void Name_ShouldNotHaveError_IfMaxLength(string name)
        {
            var dto = new RegisterUserDto { Name = name, UserName = "u", Password = "Aa123456", Email = "m@m.m", Role = UserRole.Administrator };
            var result = this.validator.TestValidate(dto);
            if (string.IsNullOrWhiteSpace(name))
            {
                result.ShouldHaveValidationErrorFor(x => x.Name);
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Name);
            }
        }

        /// <summary>
        /// Tests that the phone number field validates correctly when provided.
        /// Phone number is optional, but when provided must follow a valid format with minimum length requirements.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("+380991234567", true)]
        [InlineData("12345", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        public void PhoneNumber_Should_Validate_IfPresent(string phone, bool isValid)
        {
            var dto = new RegisterUserDto { PhoneNumber = phone, Name = "Name", UserName = "u", Password = "Aa123456", Email = "m@m.m", Role = UserRole.Administrator };
            var result = this.validator.TestValidate(dto);
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
            }
        }

        /// <summary>
        /// Tests that the role field accepts all valid user roles defined in <see cref="UserRole"/> enum.
        /// Ensures that Administrator, MainAdministrator, and Moderator roles are all considered valid.
        /// </summary>
        /// <param name="role">The user role to validate.</param>
        [Theory]
        [InlineData(UserRole.Administrator)]
        [InlineData(UserRole.MainAdministrator)]
        [InlineData(UserRole.Moderator)]
        public void Role_Should_BeValid(UserRole role)
        {
            var dto = new RegisterUserDto { Role = role, Name = "Name", UserName = "u", Password = "Aa123456", Email = "m@m.m" };
            var result = this.validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Role);
        }
    }
}