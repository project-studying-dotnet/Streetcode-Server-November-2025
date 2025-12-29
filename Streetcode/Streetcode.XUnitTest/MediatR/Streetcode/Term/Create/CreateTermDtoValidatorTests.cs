using FluentValidation.TestHelper;
using Streetcode.BLL;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.MediatR.Term.Create;
using Streetcode.BLL.Util.Validators;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Term.Create;

public class CreateTermDtoValidatorTests
{
    private readonly CreateTermDtoValidator validator;

    public CreateTermDtoValidatorTests()
    {
        this.validator = new CreateTermDtoValidator();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Dto_Is_Valid()
    {
        var dto = new TermDto
        {
            Title = "Valid Title", Description = "Valid Description"
        };

        var result = this.validator.TestValidate(dto);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Should_Have_Error_When_Title_Is_Empty(string title)
    {
        var dto = new TermDto { Title = title };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Should_Have_Error_When_Title_Exceeds_MaxLength()
    {
        var longTitle = new string('a', ValidationConstants.Term.TitleMaxLength + 1);
        var dto = new TermDto { Title = longTitle };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage(string.Format(
                ErrorMessages.RelatedTermWordTooLong,
                ValidationConstants.Term.TitleMaxLength));
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        var dto = new TermDto { Description = "" };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Description)
            .WithErrorMessage("Назва опису є обов'язковою");
    }

    [Fact]
    public void Should_Allow_Ukrainian_Characters_In_Title()
    {
        var dto = new TermDto { Title = "Гетьманщина-1649", Description = "Опис" };

        var result = this.validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}