using TicketService.API.Shared.Auth;
using TicketService.API.Shared.Networking;

namespace TicketService.API.Features.Tickets.TicketSearch;

public sealed class TicketSearch
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tickets", async (string? searchFor,
        ILoggerFactory loggerFactory,
        ITicketSearchApiClient ticketSearchApiClient,
        CancellationToken cancellationToken) =>
        {
            var logger = loggerFactory.CreateLogger(typeof(TicketSearch));
            logger.LogInformation("Ticket Search API Called");

            var resultFromExternalApi = await ticketSearchApiClient.GetTicketsAsync(searchFor, cancellationToken);
            var result = resultFromExternalApi.Select(ticket => 
            {
                return new
                {
                    ticket.Title, 
                    ticket.Description, 
                    ticket.Status,
                    ticket.Priority
                };
            });

            return Results.Ok(result);
        })
        .RequireAuthorization(AuthPolicies.Volunteer);
    }
}