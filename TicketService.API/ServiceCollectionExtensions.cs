using TicketService.API.Shared.Networking;
using TicketService.API.Features.Tickets.SearchTickets;
using System.Reflection;
using MediatR;
using TicketService.API.Shared.Behaviors;

namespace TicketService.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITicketSearchApiClient, TicketSearchApiClient>();
        services.AddSingleton<TicketSearch>();
        
        var currentAssembly = Assembly.GetExecutingAssembly();
        
        // Add MediatR with assembly scanning
        services.AddMediatR(currentAssembly);
        
        // Add the logging behavior as a pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        
        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services)
    {
        return services;
    }
}
