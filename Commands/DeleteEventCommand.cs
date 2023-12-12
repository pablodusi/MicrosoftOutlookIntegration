using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using MicrosoftOutlookIntegration.DTOs;

public class DeleteEventCommand
{
    public string EventId { get; set; } 
    public async Task<DeleteEventDto> ExecuteAsync()
    {
        DeleteEventDto deleteEventDto = new DeleteEventDto();
        try
        {
            using var connection = new SqlConnection("Data Source=localhost;Initial Catalog=MSOutlookIntegration;Integrated Security=True;Encrypt=False");
            connection.Open();

            var command = new SqlCommand(
                "DELETE FROM Event WHERE EventId=@eventId",
                connection);

            command.Parameters.AddWithValue("@eventId", EventId);

            var result = await command.ExecuteNonQueryAsync();

            if (result == 1)
            {
                deleteEventDto.couldDelete = true;
            }
            else
            {
                deleteEventDto.couldDelete = false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception appropriately (log, error message)
            
        }
        return deleteEventDto;
    }

}
