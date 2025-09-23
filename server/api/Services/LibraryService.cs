using System.ComponentModel.DataAnnotations;
using api.Dtos;
using api.Dtos.Requests;
using efscaffold;
using efscaffold.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class LibraryService(MyDbContext ctx) : ILibraryService
{
    public Task<List<AuthorDto>> GetAllAuthors()
    {
        return ctx.Authors.Select(a => new AuthorDto(a)).ToListAsync();
    }
    
    public Task<List<BookDto>> GetAllBooks()
    {
        return ctx.Books.Select(b => new BookDto(b)).ToListAsync();
    }

    public Task<List<GenreDto>> GetAllGenres()
    {
        return ctx.Genres.Select(g => new GenreDto(g)).ToListAsync();
    }
    
    public async Task<AuthorDto> CreateAuthor(CreateAuthorRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto));
        
        var author = new Author()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };
        ctx.Authors.Add(author);
        await ctx.SaveChangesAsync(); 
        return new AuthorDto(author);
    }

    public async Task<AuthorDto> UpdateAuthor(UpdateAuthorRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto));
        var author = ctx.Authors.First(a => a.Id == dto.AuthorId);
        author.Name = dto.Name;
        author.Books = dto.BooksIds.Select(id => ctx.Books.First(b => b.Id == id)).ToList();
        await ctx.SaveChangesAsync();
        return new AuthorDto(author);
    }

    public async Task<AuthorDto> DeleteAuthor(string authorId)
    {
        var author = ctx.Authors.First(a => a.Id == authorId);
        ctx.Authors.Remove(author);
        await ctx.SaveChangesAsync();
        return new AuthorDto(author);
    }

    public async Task<BookDto> CreateBook(CreateBookRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        
        var book = new Book()
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Pages = dto.Pages,
            Createdat = DateTime.UtcNow
        };
        ctx.Books.Add(book);
        await ctx.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<BookDto> UpdateBook(UpdateBookRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto));
        var book = ctx.Books.First(b=> b.Id == dto.BookId);
        var genre = ctx.Genres.First(g => g.Id == dto.GenreId);
        book.Genre = genre;
        book.Title = dto.Title;
        book.Pages = dto.Pages;
        book.Authors = dto.AuthorsIds.Select(id =>  ctx.Authors.First(a => a.Id == id)).ToList();
        ctx.Books.Update(book);
        await ctx.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<BookDto> DeleteBook(string bookId)
    {
        var book = ctx.Books.First(b => b.Id == bookId);
        ctx.Books.Remove(book);
        await ctx.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<GenreDto> CreateGenre(CreateGenreRequestDto dto)
    {
        var genre = new Genre()
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };
        ctx.Genres.Add(genre);
        await ctx.SaveChangesAsync();
        return new  GenreDto(genre);
    }

    public async Task<GenreDto> UpdateGenre(UpdateGenreRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto));
        var genre = ctx.Genres.First(g => g.Id == dto.GenreId);
        genre.Name = dto.Name;
        genre.Books = dto.BooksIds.Select(id => ctx.Books.First(b => b.Id == id)).ToList();
        await ctx.SaveChangesAsync();
        return new GenreDto(genre);
    }

    public async Task<GenreDto> DeleteGenre(string genreId)
    {
        var genre = ctx.Genres.First(g => g.Id == genreId);
        ctx.Genres.Remove(genre);
        await ctx.SaveChangesAsync();
        return new GenreDto(genre);    }
}