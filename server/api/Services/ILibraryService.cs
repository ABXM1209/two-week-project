using api.Controllers;
using api.Dtos;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Services;

public interface ILibraryService
{
    Task<Author> CreateAuthor(CreateAuthorDto createAuthorDto);
    Task<Book> CreateBook(CreateBookDto createBookDto);
    Task<Genre> CreateGenre(CreateGenreDto createGenreDto);
    Task<List<Author>> GetAllAuthors();
    Task<List<Book>> GetAllBooks();
    Task<List<Genre>> GetAllGenres();
}