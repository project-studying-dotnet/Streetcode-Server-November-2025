namespace Streetcode.XUnitTest.MediatR.Newss.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.News;
 using global::Streetcode.BLL.MediatR.Newss.Create;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class NewsDtoValidatorTests
    {
        private readonly NewsDtoValidator _validator;

        public NewsDtoValidatorTests()
        {
            _validator = new NewsDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Title_Is_Empty(string title)
        {
            var dto = new NewsDto { Title = title };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Long()
        {
            var dto = new NewsDto
            {
                Title = new string('a', ValidationConstants.News.TitleMaxLength + 1)
            };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Text_Is_Empty(string text)
        {
            var dto = new NewsDto { Text = text };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Text);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_URL_Is_Empty(string url)
        {
            var dto = new NewsDto { URL = url };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.URL);
        }

        [Fact]
        public void Should_Have_Error_When_URL_Is_Too_Long()
        {
            var dto = new NewsDto
            {
                URL = new string('a', ValidationConstants.News.UrlMaxLength + 1)
            };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.URL);
        }

        [Fact]
        public void Should_Not_Have_Error_When_ImageId_Is_Null()
        {
            var dto = new NewsDto { ImageId = null };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.ImageId);
        }

        [Fact]
        public void Should_Have_Error_When_CreationDate_Is_Empty()
        {
            var dto = new NewsDto { CreationDate = default(DateTime) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.CreationDate);
        }

        [Fact]
        public void Should_Have_Error_When_CreationDate_Is_In_Future()
        {
            var dto = new NewsDto { CreationDate = DateTime.UtcNow.AddDays(1) };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.CreationDate);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            var dto = new NewsDto
            {
                Title = "Valid Title",
                Text = "Valid Text",
                URL = "valid-url",
                ImageId = 1,
                CreationDate = DateTime.UtcNow.AddMinutes(-1)
            };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}