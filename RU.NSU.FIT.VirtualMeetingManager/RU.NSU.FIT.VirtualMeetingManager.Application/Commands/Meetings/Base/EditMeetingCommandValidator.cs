using RU.NSU.FIT.VirtualManager.Domain.Constants;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.Base;

public class EditMeetingCommandValidator
{
    public bool TryValidate<TCommand>(TCommand command, out IList<string> errors)
        where TCommand : IEditMeetingCommand
    {
        errors = new List<string>();
        
        // Проверяем название
        if (command.Name == null!
            || command.Name.Length < EntityConstants.Meeting.Name.Min
            || command.Name.Length > EntityConstants.Meeting.Name.Max)
        {
            errors.Add("Некорректное название");
        }
        
        // Проверяем даты
        if (command.EndDate < command.StartDate)
        {
            errors.Add("Некорректные даты");
        }
            
        // Проверяем описание
        if (command.Description == null!
            || command.Description.Length < EntityConstants.Meeting.Description.Min
            || command.Description.Length > EntityConstants.Meeting.Description.Max)
        {
            errors.Add("Некорректное описание");
        }
        
        // Проверяем краткое описание
        if (command.ShortDescription is not null 
            && (command.ShortDescription.Length < EntityConstants.Meeting.ShortDescription.Min
                || command.ShortDescription.Length > EntityConstants.Meeting.ShortDescription.Max))
        {
            errors.Add("Некорректное краткое описание");
        }

        // Проверяем ограничение по полу
        if (command.Gender is not null && Enum.IsDefined(command.Gender.Value) is false)
        {
            errors.Add("Некорректный пол");
        }
        
        // Проверяем ограничение на количество посетителей
        if (command.MaxUsers < 0)
        {
            errors.Add("Некорректное ограничение на количество посетителей");
        }
        
        // Проверяем ограничение на возраст
        if (command.MinAge < 0)
        {
            errors.Add("Некорректное ограничение на возраст");
        }

        return errors.Count == 0;
    }

    public void ValidateAndThrow<TCommand>(TCommand command)
        where TCommand : IEditMeetingCommand
    {
        if (TryValidate(command, out var errors) is false)
        {
            throw new BadRequestException(string.Join("; ", errors));
        }
    }
}