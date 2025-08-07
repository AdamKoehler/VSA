using TicketService.API.Shared.Domain.Models;

namespace TicketService.API.Shared.Networking
{
    public interface ITicketSearchApiClient
    {
        Task<IEnumerable<Ticket>> GetTicketsAsync(string? searchFor, CancellationToken? cancellationToken);
    }
}