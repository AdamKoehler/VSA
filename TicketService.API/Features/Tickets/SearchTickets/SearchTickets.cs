using TicketService.API.Shared.Networking;

namespace TicketService.API.Features.Tickets.SearchTickets;

public static class SearchTickets 
{
    public static void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tickets",
            async (string? searchFor,
                ILoggerFactory logger,
                CancellationToken cancellationToken,
                ITicketSearchApiClient ticketSearchApiClient) =>
            {
                logger.CreateLogger("EndpointHandlers").LogInformation("Search Tickets Feature called.");

                var resultFromApiCall = await ticketSearchApiClient.GetTicketsAsync(searchFor, cancellationToken);

                var result = resultFromApiCall.Select(t => new 
                {
                    t.Category,
                    t.Title,
                    t.Description,
                    t.CreatedByUserId
                });
               
                return Results.Ok(result);
            });
    }
}
