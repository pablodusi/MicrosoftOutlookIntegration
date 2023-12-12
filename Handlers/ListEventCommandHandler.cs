using MicrosoftOutlookIntegration.DTOs;
using System.Diagnostics.Tracing;

public class ListEventCommandHandler
{
    private readonly ListEventQuery _eventQuery;

    public ListEventCommandHandler(ListEventQuery eventQuery)
    {
        _eventQuery = eventQuery;
    }

    public async Task<EventDto> HandleAsync(ListEventQuery command)
    {
        // Valida la información de entrada
        if (string.IsNullOrEmpty(command.Subject))
        {
            throw new ArgumentException("El asunto del evento no puede estar vacío.");
        }

        if (command.InitialHour == null)
        {
            throw new ArgumentException("La hora de inicio del evento no puede ser nula.");
        }

        if (command.EndHour == null)
        {
            throw new ArgumentException("La hora de inicio del evento no puede ser nula.");
        }

        if (command.Location == null)
        {
            throw new ArgumentException("La ubicación del evento no puede ser nula.");
        }

        // Ejecuta el comando
        return await _eventQuery.ExecuteAsync();
    }
}
