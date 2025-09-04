using FastEndpoints;

namespace LibraryAPI.Endpoints.Health;

public class HealthCheckEndpoint : EndpointWithoutRequest<HealthCheckResponse>
{
    public override void Configure()
    {
        Get("/health");
        AllowAnonymous();
        Description(d => d
            .WithName("Health Check")
            .WithTags("Health")
            .Produces<HealthCheckResponse>(200, "application/json")
            .WithSummary("Check the health status of the API")
            .WithDescription("Returns the current health status and timestamp of the API"));
    }

    public override async Task<HealthCheckResponse> ExecuteAsync(CancellationToken ct)
    {
        return new HealthCheckResponse
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0"
        };
    }
}

public class HealthCheckResponse
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Version { get; set; } = string.Empty;
}
