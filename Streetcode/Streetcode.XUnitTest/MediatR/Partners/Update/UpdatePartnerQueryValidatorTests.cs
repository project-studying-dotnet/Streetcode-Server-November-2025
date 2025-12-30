namespace Streetcode.XUnitTest.MediatR.Partners.Update
{
    using System.Linq.Expressions;
    using FluentValidation.TestHelper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Partners;
 using global::Streetcode.BLL.DTO.Streetcode;
 using global::Streetcode.BLL.MediatR.Partners.Update;
 using global::Streetcode.DAL.Entities.Partners;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class UpdatePartnerQueryValidatorTests
    {
        private readonly UpdatePartnerQueryValidator _validator;
        private readonly Mock<IRepositoryWrapper> _mockRepo;

        public UpdatePartnerQueryValidatorTests()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _validator = new UpdatePartnerQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Partner_Is_Null()
        {
            var query = new UpdatePartnerCommand(null);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Partner);
        }

        [Fact]
        public void Should_Have_Error_When_Partner_Id_Is_Invalid()
        {
            var dto = new CreatePartnerDto { Id = 0, Title = "Valid" };
            var query = new UpdatePartnerCommand(dto);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Partner.Id);
        }

        [Fact]
        public async Task Should_Have_Error_When_Title_Is_Taken_By_Another_Partner()
        {
            // Arrange
            var existingId = 1;
            var myId = 2;
            var title = "Taken Title";

            var dto = new CreatePartnerDto { Id = myId, Title = title };
            var query = new UpdatePartnerCommand(dto);

            _mockRepo.Setup(r => r.PartnersRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Partner, bool>>>(),
                It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(new Partner { Id = existingId, Title = title });

            var asyncValidator = new UpdatePartnerQueryValidator(_mockRepo.Object);

            // Act
            var result = await asyncValidator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Partner)
                  .WithErrorMessage(ErrorMessages.PartnerTitleAlreadyExists);
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_Title_Belongs_To_Same_Partner()
        {
            // Arrange
            var myId = 1;
            var title = "My Own Title";

            var dto = new CreatePartnerDto
            {
                Id = myId,
                Title = title,
                TargetUrl = "https://test.com",
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
            };
            var query = new UpdatePartnerCommand(dto);

            _mockRepo.Setup(r => r.PartnersRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Partner, bool>>>(),
                It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner?)null);

            var asyncValidator = new UpdatePartnerQueryValidator(_mockRepo.Object);

            // Act
            var result = await asyncValidator.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}