using FastEndpoints;
using LibraryAPI.Endpoints.Books;

namespace LibraryAPI.Endpoints.Books;

public class GetAllBooksEndpoint : EndpointWithoutRequest<IEnumerable<BookResponse>>
{
    private readonly IBookService _bookService;

    public GetAllBooksEndpoint(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
        Description(d => d
            .WithName("Get All Books")
            .WithTags("Books")
            .Produces<IEnumerable<BookResponse>>(200, "application/json")
            .WithSummary("Retrieve all books")
            .WithDescription("Returns a list of all books in the library"));
    }

    public override async Task<IEnumerable<BookResponse>> ExecuteAsync(CancellationToken ct)
    {
        var books = await _bookService.GetAllBooksAsync();
        return books.Select(MapToResponse);
    }

    private static BookResponse MapToResponse(Book book)
    {
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
