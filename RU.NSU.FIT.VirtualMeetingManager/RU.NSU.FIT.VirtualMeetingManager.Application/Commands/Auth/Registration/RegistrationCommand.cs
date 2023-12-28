using System.Text.RegularExpressions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Constants;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Base.EditUserCommand;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;


namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Registration;

public class RegistrationCommand : IRequest<AuthResponse>, IEditUserCommand
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Phone { get; init; }
    public Queries.Base.GenderType Gender { get; init; }
    public DateOnly BirthDate { get; init; }

    [UsedImplicitly]
    public sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponse>
    {
        private static readonly Regex EmailRegex = new(
            "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])",
            RegexOptions.Compiled, TimeSpan.FromSeconds(10));
        
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly IAuthService _authService;

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly EditUserCommandValidator _commandValidator;

        public RegistrationCommandHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IAuthService authService, 
            IDateTimeProvider dateTimeProvider,
            EditUserCommandValidator commandValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _dateTimeProvider = dateTimeProvider;
            _commandValidator = commandValidator;
        }

        public async Task<AuthResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken: cancellationToken))
            {
                throw new BadRequestException("Пользователь уже существует");
            }

            //default role 'User'
            var role = await _roleManager.FindByNameAsync(RoleConstants.User);

            if (role == null)
            {
                throw new InternalServerException("Роль User не существует");
            }
            
            ValidateCommand(request);

            var newUser = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                request.BirthDate,
                _dateTimeProvider.UtcNow,
                (GenderType) request.Gender,
                request.Phone)
            {
                UserName = request.Email
            };

            newUser.AddRole(role);

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded is false)
            {
                throw new BadRequestException(
                    $"Ошибки при регистрации нового пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var response = await _authService.SignInAsync(request.Email, request.Password, cancellationToken);

            return new AuthResponse(response.AccessToken, response.RefreshToken);
        }

        private void ValidateCommand(RegistrationCommand command)
        {
            // Проверяем почту
            if (EmailRegex.IsMatch(command.Email) is false)
            {
                throw new BadRequestException("Невалидный формат Email");
            }

            // Проверяем пароль
            if (command.Password is null
                || command.Password.Length < EntityConstants.User.Password.Min
                || command.Password.Length > EntityConstants.User.Password.Max)
            {
                throw new BadRequestException("Некорректная длина пароля");
            }
            
            // Проверяем остальные поля
            _commandValidator.ValidateAndThrow(command);
        }
    }
}