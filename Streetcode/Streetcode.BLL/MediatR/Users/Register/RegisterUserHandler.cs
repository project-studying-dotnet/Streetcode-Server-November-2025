using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerService _logger;

        public RegisterUserHandler(
            UserManager<User> userManager,
            IMapper mapper,
            ILoggerService logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<RegisterUserResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(request.newUser.UserName);
                if (existingUser != null)
                {
                    return Result.Fail("User already exists");
                }

                var newUser = _mapper.Map<User>(request.newUser);
                var createResult = await _userManager.CreateAsync(newUser, request.newUser.Password);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    return Result.Fail(errors);
                }

                string roleName = request.newUser.Role.ToString();
                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, roleName);

                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    return Result.Fail($"Failed to assign role: {errors}");
                }

                var createdUser = _mapper.Map<RegisterUserResponseDto>(newUser);
                createdUser.Role = request.newUser.Role;
                return Result.Ok(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}