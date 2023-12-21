using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualManager.Domain.Constants;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Base.EditUserCommand;

public class EditUserCommandValidator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public EditUserCommandValidator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public bool TryValidate<TCommand>(TCommand command, out IList<string> errors)
        where TCommand : IEditUserCommand
    {
        errors = new List<string>();

        // Проверяем имя
        if (command.FirstName == null!
            || command.FirstName.Length < EntityConstants.User.FirstName.Min
            || command.FirstName.Length > EntityConstants.User.FirstName.Max)
        {
            errors.Add("Некорректное имя");
        }

        // Проверяем фамилию
        if (command.LastName == null!
            || command.LastName.Length < EntityConstants.User.LastName.Min
            || command.LastName.Length > EntityConstants.User.LastName.Max)
        {
            errors.Add("Некорректное имя");
        }

        // Проверяем пол
        if (Enum.IsDefined(command.Gender) is false)
        {
            errors.Add("Некорректный пол");
        }

        // Проверяем дату рождения. Зарегистрироваться можно с 14 лет
        var maxBirthDate = DateOnly.FromDateTime(_dateTimeProvider.UtcNow).AddYears(EntityConstants.User.Age.Min);

        if (command.BirthDate < EntityConstants.User.BirthDateMin || command.BirthDate > maxBirthDate)
        {
            errors.Add("Некорректная дата рождения");
        }

        // Проверяем телефон
        if (command.Phone is not null
            && (command.Phone.Length < EntityConstants.User.PhoneNumber.Min
                || command.Phone.Length > EntityConstants.User.PhoneNumber.Max))
        {
            errors.Add("Некорректная длина телефонного номера");
        }

        return errors.Count == 0;
    }

    public void ValidateAndThrow<TCommand>(TCommand command)
        where TCommand : IEditUserCommand
    {
        if (TryValidate(command, out var errors) is false)
        {
            throw new BadRequestException(string.Join("; ", errors));
        }
    }
}