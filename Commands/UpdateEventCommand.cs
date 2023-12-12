using Microsoft.Data.SqlClient;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using MicrosoftOutlookIntegration.DTOs;


public class UpdateEventCommand
{
    public string EventId { get; set; }
    public string UserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public string Location { get; set; }
    public List<Attendee> Attendees { get; set; }
    public string Notes { get; set; }

    public async Task<UpdateEventDto> ExecuteAsync()
    {
        UpdateEventDto updateEventDto = new UpdateEventDto();

        try
        {
            using var connection = new SqlConnection("Data Source=localhost;Initial Catalog=MSOutlookIntegration;Integrated Security=True;Encrypt=False");
            connection.Open();

            var command = new SqlCommand(
                "UPDATE Event SET Subject=@subject, Body=@body, InitialHour=@startTime, EndHour=@endTime, Location=@location, Notas=@notes WHERE EventId=@eventId",
                connection);

            command.Parameters.AddWithValue("@eventId", EventId);
            command.Parameters.AddWithValue("@subject", Subject);
            command.Parameters.AddWithValue("@body", Body);
            command.Parameters.AddWithValue("@startTime", StartTime);
            command.Parameters.AddWithValue("@endTime", EndTime);
            command.Parameters.AddWithValue("@location", Location);
            command.Parameters.AddWithValue("@notes", Notes);

            var result = await command.ExecuteNonQueryAsync();
            
        }
        catch (Exception ex)
        {

        }

        return updateEventDto;
    }
}
