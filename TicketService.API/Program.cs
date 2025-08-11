using Serilog;
using TicketService.API;
using TicketService.API.Middleware;
using Microsoft.AspNetCore.Authentication.Negotiate;
using TicketService.API.Shared.Auth;
using TicketService.API.Shared.Domain;
using TicketService.API.Shared.Domain.Models;
using TicketService.API.Features.Tickets.SearchTickets;

var builder = WebApplication.CreateBuilder(args);

// serilog config
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistenceServices();

// add authentication and authorization to container
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddAuthorizationBuilder().AddPolicy(AuthPolicies.BeyondTrust, policy => policy.RequireRole(AuthRoles.BeyondTrust));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}
app.UseStatusCodePages();

// serilog hook into http request pipeline
app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();

// Add endpoint
var ticketSearch = app.Services.GetRequiredService<TicketSearch>();
ticketSearch.AddEndpoint(app);


app.Run();
