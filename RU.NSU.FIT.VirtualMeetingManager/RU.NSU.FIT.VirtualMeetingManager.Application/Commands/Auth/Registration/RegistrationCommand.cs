using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;


namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Registration;

public class RegistrationCommand : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Queries.Base.GenderType Gender { get; set; }
    public DateOnly BirthDate { get; set; }

    [UsedImplicitly]
    public sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponse>
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly IAuthService _authService;

        private readonly IDateTimeProvider _dateTimeProvider;

        public RegistrationCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager,
            IAuthService authService, IDateTimeProvider dateTimeProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<AuthResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken: cancellationToken))
            {
                throw new BadRequestException("Пользователь уже существует");
            }

            //default role 'User'
            var role = await _roleManager.FindByNameAsync(RoleType.User.ToString());

            if (role == null)
            {
                throw new InternalServerException("Роль User не существует");
            }

            var newUser = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                request.BirthDate,
                _dateTimeProvider.UtcNow,
                (GenderType) request.Gender)
            {
                UserName = request.Email
            };

            newUser.AddRole(role);

            await _userManager.CreateAsync(newUser, request.Password);

            var response = await _authService.SignInAsync(request.Email, request.Password, cancellationToken);

            return new AuthResponse(response.AccessToken, response.RefreshToken);
        }
    }
}