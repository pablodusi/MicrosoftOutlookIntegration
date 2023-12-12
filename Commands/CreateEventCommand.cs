using MicrosoftOutlookIntegration.DTOs;

using Microsoft.Data.SqlClient;
using Microsoft.Graph.Models;

public class CreateEventCommand
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

    public async Task<CreateEventDto> ExecuteAsync()
    {
        // Crea una conexión a la base de datos
        var connection = new SqlConnection("Data Source=localhost;Initial Catalog=MSOutlookIntegration;Integrated Security=True;Encrypt=False");
        connection.Open();

        // Crea una consulta para insertar el evento
        var query = new SqlCommand
        {
            CommandText = "INSERT INTO Event (EventId,UserId,Subject, Body,InitialHour, EndHour, Location, Notas) VALUES (@eventid,@userId,@subject, @body,@startTime, @endTime, @location, @notes)",
            Connection = connection,
        };

        // Establece los parámetros de la consulta<
        query.Parameters.AddWithValue("@eventId", EventId);
        query.Parameters.AddWithValue("@userId", UserId);
        query.Parameters.AddWithValue("@subject", Subject);
        query.Parameters.AddWithValue("@body", Body);
        query.Parameters.AddWithValue("@startTime", StartTime);
        query.Parameters.AddWithValue("@endTime", EndTime);
        query.Parameters.AddWithValue("@location", Location);
        query.Parameters.AddWithValue("@notes", Notes);

        // Ejecuta la consulta
        var result = await query.ExecuteNonQueryAsync();

        // Cierra la conexión a la base de datos
        connection.Close();
        CreateEventDto createEventDto = new CreateEventDto();
        // Verifica el resultado
        if (result == 1)
        {
            createEventDto.couldCreate = true;
        }
        else
        {
            createEventDto.couldCreate = false;
        }

        return createEventDto;
    }
}
