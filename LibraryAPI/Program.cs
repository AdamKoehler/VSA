using FastEndpoints;
using LibraryAPI.Endpoints.Books;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddFastEndpoints();

// Register application services
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseFastEndpoints();

app.Run();
