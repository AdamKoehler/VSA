namespace LibraryAPI.Endpoints.Books;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(CreateBookRequest request);
    Task<Book?> UpdateBookAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteBookAsync(int id);
}

public class BookService : IBookService
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public BookService()
    {
        // Add some sample books
        _books.Add(new Book
        {
            Id = _nextId++,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            ISBN = "978-0743273565",
            PublicationYear = 1925,
            Genre = "Classic",
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        });

        _books.Add(new Book
        {
            Id = _nextId++,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            ISBN = "978-0446310789",
            PublicationYear = 1960,
            Genre = "Classic",
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        });

        _books.Add(new Book
        {
            Id = _nextId++,
            Title = "1984",
            Author = "George Orwell",
            ISBN = "978-0451524935",
            PublicationYear = 1949,
            Genre = "Dystopian",
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        });
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<Book> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Id = _nextId++,
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            PublicationYear = request.PublicationYear,
            Genre = request.Genre,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task<Book?> UpdateBookAsync(int id, UpdateBookRequest request)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult<Book?>(null);

        book.Title = request.Title;
        book.Author = request.Author;
        book.ISBN = request.ISBN;
        book.PublicationYear = request.PublicationYear;
        book.Genre = request.Genre;
        book.IsAvailable = request.IsAvailable;
        book.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult<Book?>(book);
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult(false);

        _books.Remove(book);
        return Task.FromResult(true);
    }
}
