namespace TicketService.API.Shared.Behaviors
{
    public class LoggingBehavior<TRequest>(ILogger<TRequest> logger) : IRequestPreProccessor<TRequest>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger = logger;
        public Task Process(TRequest request, CancellationToken cancellationToken)
        { 
            _logger.LogInformation("Starting {featureFromRequestName}",
                typeof(TRequest).Name);
            return Task.CompletedTask;
        }
    }
}
