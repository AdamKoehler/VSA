using Microsoft.Extensions.Logging;
using MediatR;

namespace TicketService.API.Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        { 
            _logger.LogInformation("Starting request: {RequestType} at {Timestamp}", 
                typeof(TRequest).Name, 
                DateTime.UtcNow);
            
            var response = await next();
            
            _logger.LogInformation("Completed request: {RequestType} at {Timestamp}", 
                typeof(TRequest).Name, 
                DateTime.UtcNow);
                
            return response;
        }
    }
}
