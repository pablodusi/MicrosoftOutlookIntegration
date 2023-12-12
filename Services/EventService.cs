using MicrosoftOutlookIntegrationApp.Entities;
using Microsoft.Graph;
using Azure.Identity;
using Microsoft.Graph.Models;
using NuGet.Protocol;

namespace MicrosoftOutlookIntegration.Services
{
    public static class EventService
    {
        public static async Task<bool> AddEventAsync(MicrosoftOutlookIntegrationApp.Entities.Event evento)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientId = "e8863538-3865-45f6-8e35-d0658ddc51ca";
            var tenantId = "e6ec5bf7-5419-45c8-8e13-be505043cc09";
            var clientSecret = "-Xt8Q~ihh.B6mhL3xLwyS.8HsSxrDFh09iNU6aRG";
            
            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var requestBody = new Microsoft.Graph.Models.Event
            {
                Subject = evento.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = evento.Body,
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = "2023-12-11T12:00:00",
                    TimeZone = "Pacific Standard Time",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = "2023-12-11T14:00:00",
                    TimeZone = "Pacific Standard Time",
                },
                Location = new Location
                {
                    DisplayName = evento.Location,
                },
                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "pablodusi.tots@outlook.com",
                            Name = "Pablo Dusi",
                        },
                        Type = AttendeeType.Optional,
                    }
            },
                AllowNewTimeProposals = true,
                TransactionId = "7E163156-7762-4BEB-A1C6-729EA81755A7",
            };
            try
            {
                var result = await graphClient.Users["ce9a4d2b-c063-4ed1-98b0-d7f2e10712e3"].Events.PostAsync(requestBody, (requestConfiguration) =>
                {
                    requestConfiguration.Headers.Add("Prefer", "outlook.timezone=\"Pacific Standard Time\"");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


            return true;
        }

        public static async Task<bool> DeleteEventAsync(string id)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientId = "e8863538-3865-45f6-8e35-d0658ddc51ca";
            var tenantId = "e6ec5bf7-5419-45c8-8e13-be505043cc09";
            var clientSecret = "-Xt8Q~ihh.B6mhL3xLwyS.8HsSxrDFh09iNU6aRG";

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            try
            {
                await graphClient.Users["ce9a4d2b-c063-4ed1-98b0-d7f2e10712e3"].Events[id].DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return true;
        }

        public static async Task<bool> UpdateEventAsync(MicrosoftOutlookIntegrationApp.Entities.Event evento)
        {
            DeleteEventAsync(evento.EventIdCalendar);
            EventService.AddEventAsync(evento);
            return true;
        }

        public static async Task<bool> ListEventAsync(MicrosoftOutlookIntegrationApp.Entities.Event evento)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientId = "e8863538-3865-45f6-8e35-d0658ddc51ca";
            var tenantId = "e6ec5bf7-5419-45c8-8e13-be505043cc09";
            var clientSecret = "-Xt8Q~ihh.B6mhL3xLwyS.8HsSxrDFh09iNU6aRG";

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var requestBody = new Microsoft.Graph.Models.Event
            {
                Subject = evento.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = evento.Body,
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = "2023-12-11T12:00:00",
                    TimeZone = "Pacific Standard Time",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = "2023-12-11T14:00:00",
                    TimeZone = "Pacific Standard Time",
                },
                Location = new Location
                {
                    DisplayName = evento.Location,
                },
                AllowNewTimeProposals = true,
                TransactionId = "7E163156-7762-4BEB-A1C6-729EA81755A7",
            };
            try
            {
                var result = await graphClient.Users["ce9a4d2b-c063-4ed1-98b0-d7f2e10712e3"].Events.GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.QueryParameters.Select = new string[] { "subject", "body", "bodyPreview", "organizer", "attendees", "start", "end", "location" };
                    requestConfiguration.Headers.Add("Prefer", "outlook.timezone=\"Pacific Standard Time\"");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


            return true;
        }
    }
}

