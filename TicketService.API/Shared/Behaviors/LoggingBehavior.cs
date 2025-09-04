using MediatR;

namespace TicketService.API.Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        { 
            _logger.LogInformation("Starting {featureFromRequestName}",
                typeof(TRequest).Name);
            
            var result = await next();
            
            _logger.LogInformation("Completed {featureFromRequestName}",
                typeof(TRequest).Name);
                
            return result;
        }
    }
}
