using FluentValidation.TestHelper;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.Term.Create;
using Streetcode.BLL.Util.Validators;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Streetcode.Term.Create;

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

        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Назва терміну є обов'язковою");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Exceeds_MaxLength()
    {
        var longTitle = new string('a', ValidationConstants.Term.TitleMaxLength + 1);
        var dto = new TermDto { Title = longTitle };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage($"Назва терміну не може перевищувати {ValidationConstants.Term.TitleMaxLength} символів");
    }

    [Theory]
    [InlineData("Invalid@Title")]
    [InlineData("Title_With_Underscore")]
    [InlineData("Title!")]
    public void Should_Have_Error_When_Title_Contains_Invalid_Characters(string title)
    {
        var dto = new TermDto { Title = title };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Назва терміну може містити лише літери, цифри, пробіли та дефіси");
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
    public void Should_Have_Error_When_RelatedTerms_Contains_Zero_Or_Negative()
    {
        var dto = new TermDto { RelatedTerms = new List<int> { 5, 0, -1 } };

        var result = this.validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor("RelatedTerms[1]")
            .WithErrorMessage("ID пов'язаного терміну має бути більше 0");

        result.ShouldHaveValidationErrorFor("RelatedTerms[2]")
            .WithErrorMessage("ID пов'язаного терміну має бути більше 0");
    }

    [Fact]
    public void Should_Allow_Ukrainian_Characters_In_Title()
    {
        var dto = new TermDto { Title = "Гетьманщина-1649", Description = "Опис" };

        var result = this.validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}