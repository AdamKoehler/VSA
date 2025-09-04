using FastEndpoints;
using LibraryAPI.Endpoints.Books;

namespace LibraryAPI.Endpoints.Books;

public class GetBookByIdEndpoint : Endpoint<GetBookByIdRequest, BookResponse>
{
    private readonly IBookService _bookService;

    public GetBookByIdEndpoint(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Get("/api/books/{id}");
        AllowAnonymous();
        Description(d => d
            .WithName("Get Book by ID")
            .WithTags("Books")
            .Produces<BookResponse>(200, "application/json")
            .Produces(404, "application/json")
            .WithSummary("Retrieve a book by ID")
            .WithDescription("Returns a specific book from the library"));
    }

    public override async Task<BookResponse> ExecuteAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var book = await _bookService.GetBookByIdAsync(req.Id);
        
        if (book == null)
        {
            await SendNotFoundAsync(ct);
            return new BookResponse();
        }

        return new BookResponse
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
    }
}

public class GetBookByIdRequest
{
    public int Id { get; set; }
}
