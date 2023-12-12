using MicrosoftOutlookIntegration.DTOs;
using System.Diagnostics.Tracing;

public class DeleteEventCommandHandler
{
    private readonly DeleteEventCommand _eventCommand;
    public DeleteEventCommandHandler(DeleteEventCommand eventCommand)
    {
        _eventCommand = eventCommand;
    }
    public async Task<DeleteEventDto> HandleAsync(DeleteEventCommand command)
    { 
        return await _eventCommand.ExecuteAsync();
    }
}