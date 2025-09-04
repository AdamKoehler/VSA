using FastEndpoints;
using LibraryAPI.Endpoints.Books;

namespace LibraryAPI.Endpoints.Books;

public class CreateBookEndpoint : Endpoint<CreateBookRequest, BookResponse>
{
    private readonly IBookService _bookService;

    public CreateBookEndpoint(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Post("/api/books");
        AllowAnonymous();
        Description(d => d
            .WithName("Create Book")
            .WithTags("Books")
            .Produces<BookResponse>(201, "application/json")
            .WithSummary("Create a new book")
            .WithDescription("Adds a new book to the library"));
    }

    public override async Task<BookResponse> ExecuteAsync(CreateBookRequest req, CancellationToken ct)
    {
        var book = await _bookService.CreateBookAsync(req);
        
        var response = new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            PublicationYear = book.PublicationYear,
            Genre = book.Genre,
            IsAvailable = book.IsAvailable,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };

        await SendAsync(response, 201, ct);
        return response;
    }
}
