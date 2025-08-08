using Serilog;
using TicketService.API;
using TicketService.API.Features.Tickets.TicketSearch;
using TicketService.API.Middleware;
using Microsoft.AspNetCore.Authentication.Negotiate;
using TicketService.API.Shared.Auth;

var builder = WebApplication.CreateBuilder(args);

// serilog config
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistenceServices();

// Windows authentication https://learn.microsoft.com/en-us/aspnet/core/security/authentication/windowsauth?view=aspnetcore-9.0&tabs=visual-studio
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddAuthorizationBuilder().AddPolicy(AuthPolicies.Volunteer, policy => policy.RequireRole(AuthRoles.Volunteer));

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