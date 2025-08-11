using TicketService.API.Shared.Networking;
using TicketService.API.Features.Tickets.SearchTickets;

namespace TicketService.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITicketSearchApiClient, TicketSearchApiClient>();
        services.AddSingleton<TicketSearch>();
        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
    {
        return services;
    }
}
