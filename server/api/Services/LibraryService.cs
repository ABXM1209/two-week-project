using System.ComponentModel.DataAnnotations;
using api.Controllers;
using api.Dtos;
using efscaffold;
using efscaffold.Entities;

namespace api.Services;

public class LibraryService(MyDbContext dbContext) : ILibraryService
{
    public async Task<List<Author>> GetAllAuthors()
    {
        return dbContext.Authors.ToList();
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return dbContext.Books.ToList();
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        return dbContext.Genres.ToList();
    }
    
    public async Task<Author> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var myAuthor = new Author()
        {
            Id = Guid.NewGuid().ToString(),
            Name = createAuthorDto.Name,
            Createdat = DateTime.UtcNow
        };
        dbContext.Authors.Add(myAuthor);
        dbContext.SaveChanges(); 
        return myAuthor;
    }

    public async Task<Book> CreateBook(CreateBookDto createBookDto)
    {
        if (createBookDto.Pages < 24)
            throw new ValidationException("Invalid page number, Must be at least 24");
        var myBook = new Book()
        {
            Id = Guid.NewGuid().ToString(),
            Title = createBookDto.Title,
            Pages = createBookDto.Pages,
            Createdat = createBookDto.CreatedAt
        };
        dbContext.Books.Add(myBook);
        dbContext.SaveChanges();
        return myBook;
    }

    public async Task<Genre> CreateGenre(CreateGenreDto createGenreDto)
    {
        var myGenre = new Genre()
        {
            Id = Guid.NewGuid().ToString(),
            Name = createGenreDto.Name,
            Createdat = DateTime.UtcNow
        };
        dbContext.Genres.Add(myGenre);
        dbContext.SaveChanges();
        return myGenre;
    }
}