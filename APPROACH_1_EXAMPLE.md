# Approach #1: All Validation in Validators (Including Database Checks)

## Philosophy
- Validators handle ALL validation, including database existence checks
- Handlers assume all validation has passed and focus purely on business logic
- Handlers become thin orchestration layers
- No defensive null checks in handlers
- No try-catch blocks for validation scenarios

---

## Example: UpdateTextCommand with Database Validation

### 1. Validator with Database Access

```csharp
using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update;

public class UpdateTextCommandValidator : AbstractValidator<UpdateTextCommand>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UpdateTextCommandValidator(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Text Id must be greater than 0");

        RuleFor(x => x.Id)
            .MustAsync(TextExists)
            .WithMessage("Cannot find text with corresponding id: {PropertyValue}");

        RuleFor(x => x.Text)
            .NotNull()
            .WithMessage("Text data cannot be null");

        When(x => x.Text != null, () =>
        {
            RuleFor(x => x.Text.Title)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Text.TextContent)
                .NotEmpty()
                .MaximumLength(15000);
        });
    }

    private async Task<bool> TextExists(int id, CancellationToken cancellationToken)
    {
        var text = await _repositoryWrapper.TextRepository
            .GetFirstOrDefaultAsync(f => f.Id == id);
        return text != null;
    }
}
```

### 2. Simplified Handler (No Validation Logic)

```csharp
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update;

public class UpdateTextHandler : IRequestHandler<UpdateTextCommand, Result<TextDto>>
{
    private const string DefaultAuthorship = "Текст підготовлений спільно з";
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public UpdateTextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    public async Task<Result<TextDto>> Handle(UpdateTextCommand request, CancellationToken cancellationToken)
    {
        // No null checks - validator guarantees text exists
        var text = await _repositoryWrapper.TextRepository
            .GetFirstOrDefaultAsync(f => f.Id == request.Id);

        // Apply updates
        _mapper.Map(request.Text, text);

        // Business logic
        if (!string.IsNullOrEmpty(text.AdditionalText) && 
            text.AdditionalText.Trim() == DefaultAuthorship)
        {
            text.AdditionalText = null;
        }

        // Persist changes
        _repositoryWrapper.TextRepository.Update(text);
        await _repositoryWrapper.SaveChangesAsync();

        // Return result - no need to check SaveChanges result
        return Result.Ok(_mapper.Map<TextDto>(text));
    }
}
```

---

## Example: CreatePartnerCommand with Complex Validation

### 1. Validator with Multiple Database Checks

```csharp
using FluentValidation;
using Streetcode.BLL.DTO.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Partners.Create;

public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public CreatePartnerCommandValidator(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;

        RuleFor(x => x.Partner)
            .NotNull()
            .WithMessage("Partner data cannot be null");

        When(x => x.Partner != null, () =>
        {
            RuleFor(x => x.Partner.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Partner.TargetUrl)
                .NotEmpty()
                .Must(BeValidUrl)
                .WithMessage("Target URL must be a valid URL");

            // Database validation: Check if partner with same title already exists
            RuleFor(x => x.Partner.Title)
                .MustAsync(BeUniquePartnerTitle)
                .WithMessage("Partner with title '{PropertyValue}' already exists");

            // Database validation: Check if logo image exists
            RuleFor(x => x.Partner.LogoId)
                .MustAsync(LogoExists)
                .When(x => x.Partner.LogoId.HasValue)
                .WithMessage("Logo with id {PropertyValue} does not exist");

            // Database validation: Validate all streetcode IDs exist
            RuleForEach(x => x.Partner.TargetUrlList)
                .ChildRules(url =>
                {
                    url.RuleFor(x => x.StreetcodeId)
                        .MustAsync((context, streetcodeId, cancellation) => 
                            StreetcodeExists(streetcodeId, cancellation))
                        .When(x => x.StreetcodeId.HasValue)
                        .WithMessage("Streetcode with id {PropertyValue} does not exist");
                });
        });
    }

    private bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private async Task<bool> BeUniquePartnerTitle(string? title, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(title)) return true;

        var existingPartner = await _repositoryWrapper.PartnersRepository
            .GetFirstOrDefaultAsync(p => p.Title.ToLower() == title.ToLower());
        
        return existingPartner == null;
    }

    private async Task<bool> LogoExists(int? logoId, CancellationToken cancellationToken)
    {
        if (!logoId.HasValue) return true;

        var logo = await _repositoryWrapper.ImageRepository
            .GetFirstOrDefaultAsync(i => i.Id == logoId.Value);
        
        return logo != null;
    }

    private async Task<bool> StreetcodeExists(int? streetcodeId, CancellationToken cancellationToken)
    {
        if (!streetcodeId.HasValue) return true;

        var streetcode = await _repositoryWrapper.StreetcodeRepository
            .GetFirstOrDefaultAsync(s => s.Id == streetcodeId.Value);
        
        return streetcode != null;
    }
}
```

### 2. Ultra-Simplified Handler

```csharp
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Partners;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Partners.Create;

public class CreatePartnerHandler : IRequestHandler<CreatePartnerCommand, Result<PartnerDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public CreatePartnerHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    public async Task<Result<PartnerDto>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        // Validator has already confirmed:
        // - Partner data is not null
        // - Title is unique
        // - Logo exists (if provided)
        // - All streetcode IDs exist
        // So we just map, save, and return!

        var partner = _mapper.Map<Partner>(request.Partner);
        
        await _repositoryWrapper.PartnersRepository.CreateAsync(partner);
        await _repositoryWrapper.SaveChangesAsync();
        
        return Result.Ok(_mapper.Map<PartnerDto>(partner));
    }
}
```

---

## Pros and Cons

### ✅ Advantages of Approach #1

1. **Single Responsibility**: Validators validate, handlers execute business logic
2. **Cleaner Handlers**: Handlers become 5-10 lines of orchestration code
3. **Better Error Messages**: Validation errors are detailed and user-friendly
4. **Testability**: Can test validators independently with database mocks
5. **Fail Fast**: Invalid requests never reach handlers
6. **No Duplication**: Validation logic is centralized in one place

### ⚠️ Disadvantages of Approach #1

1. **Performance**: Database queries run BEFORE handler execution (double queries)
   - Validator: `SELECT * FROM texts WHERE id = @id` (to validate existence)
   - Handler: `SELECT * FROM texts WHERE id = @id` (to get data for update)

2. **Complexity**: Validators need repository dependencies
   - Validators become harder to write and maintain
   - More constructor dependencies

3. **Transaction Scope**: Validator queries run outside handler transaction
   - Race conditions possible between validation and execution
   - Example: Text exists during validation but deleted before handler runs

4. **Caching Challenges**: Can't easily cache the entity retrieved during validation
   - Would need complex context passing mechanism

5. **Not Always Semantic**: Some "existence checks" are really business operations
   - Example: Checking if user has permission is business logic, not validation

---

## Hybrid Approach (Best of Both Worlds)

Many teams use a hybrid:

```csharp
public class UpdateTextCommandValidator : AbstractValidator<UpdateTextCommand>
{
    public UpdateTextCommandValidator()
    {
        // Input validation only - no database
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Text).NotNull();
        RuleFor(x => x.Text.Title).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Text.TextContent).NotEmpty().MaximumLength(15000);
    }
}

public class UpdateTextHandler : IRequestHandler<UpdateTextCommand, Result<TextDto>>
{
    public async Task<Result<TextDto>> Handle(UpdateTextCommand request, CancellationToken cancellationToken)
    {
        // Handler does data integrity checks (existence)
        var text = await _repositoryWrapper.TextRepository
            .GetFirstOrDefaultAsync(f => f.Id == request.Id);

        if (text is null)
        {
            return Result.Fail("Cannot find text with corresponding id.");
        }

        // Business logic...
        _mapper.Map(request.Text, text);
        
        if (!string.IsNullOrEmpty(text.AdditionalText) && 
            text.AdditionalText.Trim() == DefaultAuthorship)
        {
            text.AdditionalText = null;
        }

        _repositoryWrapper.TextRepository.Update(text);
        var isSuccessResult = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (!isSuccessResult)
        {
            return Result.Fail("Cannot save changes in the database.");
        }

        return Result.Ok(_mapper.Map<TextDto>(text));
    }
}
```

This hybrid approach:
- ✅ Validates input structure in validators (fast, no DB)
- ✅ Checks data existence in handlers (single query)
- ✅ Keeps validators simple and fast
- ✅ Avoids double database queries
- ✅ Keeps transaction scope clean

---

## When to Use Each Approach

### Use Approach #1 (All Validation in Validators) When:
- Performance is not critical
- You want maximum separation of concerns
- Your validators can cache database lookups
- You have complex validation rules that benefit from FluentValidation's syntax

### Use Approach #2 (Current - Hybrid) When:
- Performance matters (most web applications)
- You want to avoid double database queries
- Your validation is simple (null checks, format validation)
- You need data retrieved during validation for business logic

### Rule of Thumb:
- **Static validation** (format, length, regex) → Validators
- **Database existence checks** → Handlers (current approach)
- **Complex business rules** → Handlers or separate domain services
