using Polly;
using Polly.Retry;
using System.Net.Http.Headers;
using System.Text.Json;
using TicketService.API.Shared.Domain.Models;

namespace TicketService.API.Shared.Networking;

public class TicketSearchApiClient(IHttpClientFactory httpClientFactory,
    IConfiguration configuration) : ITicketSearchApiClient
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly AsyncRetryPolicy _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    private readonly JsonSerializerOptions _jsonSerializerOptions = new (JsonSerializerDefaults.Web);

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(string? searchFor,
        CancellationToken? cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["Integrations:TicketSearchApiRoot"]}tickets?searchFor={searchFor}");
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")); 

        using (var response = await _retryPolicy.ExecuteAsync(() =>
            client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken ?? CancellationToken.None)))
        {
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync(); 
            return await JsonSerializer.DeserializeAsync<IEnumerable<Ticket>>(stream,
               _jsonSerializerOptions) ?? [];
        } 
    }
}
