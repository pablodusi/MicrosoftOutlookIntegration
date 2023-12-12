using Microsoft.Data.SqlClient;
using Microsoft.Graph.Models;
using MicrosoftOutlookIntegration.DTOs;
using MicrosoftOutlookIntegrationApp.Entities;
public class ListEventQuery
{
    public string EventId { get; set; }
    public string UserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime InitialHour { get; set; }
    public DateTime EndHour { get; set; }
    public string? Location { get; set; }
    public string? Notas { get; set; }
    public string? EventIdCalendar { get; set; }
    public async Task<EventDto> ExecuteAsync()
    {
        var events = new EventDto();

        using var connection = new SqlConnection("Data Source=localhost;Initial Catalog=MSOutlookIntegration;Integrated Security=True;Encrypt=False");
        connection.Open();

        var query = new SqlCommand
        {
            CommandText = @"
SELECT EventId, UserId, Subject, Body, InitialHour AS StartTime, EndHour AS EndTime, Location, Notas AS Notes
FROM Event
WHERE (@EventId IS NULL OR EventId = @EventId)",
            Connection = connection
        };

        if (!string.IsNullOrEmpty(EventId))
        {
            query.Parameters.AddWithValue("@EventId", EventId);
        }

        using var reader = await query.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            events.events.Add(new MicrosoftOutlookIntegrationApp.Entities.Event
            {
                EventId = reader.GetString(0),
                UserId = reader.GetString(1),
                Subject = reader.GetString(2),
                Body = reader.GetString(3),
                InitialHour = Convert.ToDateTime(reader.GetDateTimeOffset(4)),
                EndHour = Convert.ToDateTime(reader.GetDateTimeOffset(5)),
                Location = reader.GetString(6),
                Notas = reader.GetString(7)
            });
        }

        return events;
    }
}
