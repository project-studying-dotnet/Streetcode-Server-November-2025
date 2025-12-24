namespace Streetcode.XUnitTest.MediatR.Email
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Email;
    using Streetcode.BLL.MediatR.Email;
    using Xunit;

    public class EmailDtoValidatorTests
    {
        private readonly EmailDtoValidator validator;

        public static string ValidContent => new string('A', 500);

        private static string InvalidContent => new string('A', 501);

        public EmailDtoValidatorTests()
        {
            this.validator = new EmailDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetEmailFromTestData))]
        public void From_Should_Validate_Value(string from, bool isValid)
        {
            var model = new EmailDto
            {
                From = from,
                Content = "Test",
            };

            var result = this.validator.TestValidate(model);

            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.From);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.From);
            }
        }

        [Theory]
        [MemberData(nameof(GetEmailContentTestData))]
        public void Content_Should_Validate_Value(string content, bool isValid)
        {
            var model = new EmailDto
            {
                From = "john.doe@gmail.com",
                Content = content,
            };

            var result = this.validator.TestValidate(model);

            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Content);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.Content);
            }
        }

        public static IEnumerable<object[]> GetEmailFromTestData()
        {
            yield return new object[] { "", false };
            yield return new object[] { null, false };
            yield return new object[] { "UniversalTransactionalAndNotificationEmailSenderServiceShouldBeValid80@gmail.com", true }; // 80 symbols, Valid format
            yield return new object[] { "UniversalTransactionalAndNotificationEmailSenderServiceShouldNotBeValid@gmail.com", false }; // 81 symbols, Valid format
            yield return new object[] { "UniversalTransactionalAndNotificationEmailSenderServiceShouldNotBeValid", false }; // Invalid format
            yield return new object[] { "john.doe@gmail.com", true }; // Valid format
            yield return new object[] { "@gmail.com", false }; // Invalid format
            yield return new object[] { "john.doe@", false }; // Invalid format
        }

        public static IEnumerable<object[]> GetEmailContentTestData()
        {
            yield return new object[] { string.Empty, false };
            yield return new object[] { null, false };
            yield return new object[] { "q", true };
            yield return new object[] { new string('A', 500), true };
            yield return new object[] { new string('A', 501), false };
        }
    }
}