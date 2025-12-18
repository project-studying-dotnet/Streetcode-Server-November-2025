// using FluentValidation;
//
// namespace Streetcode.BLL.MediatR.Streetcode.Term.Create;
//
// public class CreateTermValidator : AbstractValidator<CreateTermCommand>
// {
//     public CreateTermValidator()
//     {
//         // Проверяем саму модель
//         RuleFor(x => x.term).NotNull().WithMessage("Данные термина не могут быть пустыми");
//
//         // Проверяем поля внутри модели
//         RuleFor(x => x.term.Title)
//             .NotEmpty().WithMessage("Заголовок обязателен")
//             .MaximumLength(100).WithMessage("Заголовок не может быть длиннее 100 символов");
//
//         RuleFor(x => x.term.Description)
//             .NotEmpty().WithMessage("Описание обязательно");
//     }
// }