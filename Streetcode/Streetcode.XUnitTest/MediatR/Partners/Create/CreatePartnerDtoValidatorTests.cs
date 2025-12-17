namespace Streetcode.XUnitTest.MediatR.Partners.Create
{
    using System.Linq.Expressions;
    using FluentValidation.TestHelper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Partners;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.MediatR.Partners.Create;
    using Streetcode.BLL.Util.Validators;
    using Streetcode.DAL.Entities.Partners;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class CreatePartnerDtoValidatorTests
    {
        private readonly CreatePartnerDtoValidator _validator;
        private readonly Mock<IRepositoryWrapper> _mockRepo;

        public CreatePartnerDtoValidatorTests()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _validator = new CreatePartnerDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var dto = new CreatePartnerDto { Title = string.Empty };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_TargetUrl_Is_Invalid()
        {
            var dto = new CreatePartnerDto { TargetUrl = "not-a-url" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.TargetUrl);
        }

        [Fact]
        public void Should_Have_Error_When_LogoId_Is_Invalid()
        {
            var dto = new CreatePartnerDto { LogoId = 0 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.LogoId);
        }

        [Fact]
        public void Should_Have_Error_When_Streetcodes_Is_Null()
        {
            var dto = new CreatePartnerDto { Streetcodes = null };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Streetcodes);
        }

        [Fact]
        public async Task Should_Have_Error_When_Title_Is_Not_Unique()
        {
            // Arrange
            var title = "Existing Partner";
            var dto = new CreatePartnerDto { Title = title };

            _mockRepo.Setup(r => r.PartnersRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Partner, bool>>>(),
                It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(new Partner { Title = title });

            var asyncValidator = new CreatePartnerDtoValidator(_mockRepo.Object);

            // Act
            var result = await asyncValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Партнер з такою назвою вже існує");
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Title_Is_Unique()
        {
            // Arrange
            var title = "New Unique Partner";
            var dto = new CreatePartnerDto
            {
                Title = title,
                TargetUrl = "https://test.com",
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };

            _mockRepo.Setup(r => r.PartnersRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Partner, bool>>>(),
                It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner?)null);

            var asyncValidator = new CreatePartnerDtoValidator(_mockRepo.Object);

            // Act
            var result = await asyncValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }
    }
}