using MicrosoftOutlookIntegration.DTOs;
using System.Diagnostics.Tracing;

public class CreateEventCommandHandler
{
    private readonly CreateEventCommand _eventCommand;

    public CreateEventCommandHandler(CreateEventCommand eventCommand)
    {
        _eventCommand = eventCommand;
    }

    public async Task<CreateEventDto> HandleAsync(CreateEventCommand command)
    {
        // Valida la informaci�n de entrada
        if (string.IsNullOrEmpty(command.Subject))
        {
            throw new ArgumentException("El asunto del evento no puede estar vac�o.");
        }

        if (command.StartTime == null)
        {
            throw new ArgumentException("La hora de inicio del evento no puede ser nula.");
        }

        if (command.EndTime == null)
        {
            throw new ArgumentException("La hora de inicio del evento no puede ser nula.");
        }

        if (command.Location == null)
        {
            throw new ArgumentException("La ubicaci�n del evento no puede ser nula.");
        }

        // Ejecuta el comando
        return await _eventCommand.ExecuteAsync();
    }
}
