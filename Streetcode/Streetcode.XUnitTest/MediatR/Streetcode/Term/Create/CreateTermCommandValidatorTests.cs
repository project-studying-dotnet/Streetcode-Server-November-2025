using FluentValidation.TestHelper;
using global::Streetcode.BLL.DTO.TextContent;
using global::Streetcode.BLL.MediatR.Term.Create;
using Xunit;

namespace Streetcode.XUnit.MediatR.Streetcode.Term.Create;

public class CreateTermCommandValidatorTests
{
    private readonly CreateTermCommandValidator validator;

    public CreateTermCommandValidatorTests()
    {
        this.validator = new CreateTermCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Term_Is_Null()
    {
        var command = new CreateTermCommand(null!);

        var result = this.validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Term).WithErrorMessage("Дані терміну не можуть бути порожніми");
    }

    [Fact]
    public void Should_Have_Child_Validator_Error_When_Dto_Is_Invalid()
    {
        var command = new CreateTermCommand(new TermDto { Title = "" });

        var result = this.validator.TestValidate(command);

        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Term_Is_Valid()
    {
        var command = new CreateTermCommand(new TermDto { Title = "Valid Title", Description = "Valid Description" });

        var result = this.validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}