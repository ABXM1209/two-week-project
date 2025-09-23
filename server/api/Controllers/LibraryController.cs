using System.ComponentModel.DataAnnotations;
using api.Dtos;
using api.Services;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class LibraryController(ILibraryService libraryService) : ControllerBase
{
    [Route(nameof(GetAllAuthors))]
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        var authors = await libraryService.GetAllAuthors();
        return authors;
    }

    [Route(nameof(CreateAuthor))]
    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor([FromBody]CreateAuthorDto createAuthorDto)
    {
        var result = await libraryService.CreateAuthor(createAuthorDto);
        return result;
    }
    
    [Route(nameof(GetAllBooks))]
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooks()
    {
        var books = await libraryService.GetAllBooks();
        return books;
    }

    [Route(nameof(CreateBook))]
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook([FromBody]CreateBookDto createBookDto)
    {
        var result = await libraryService.CreateBook(createBookDto);
        return result;
    }
    
    [Route(nameof(GetAllGenres))]
    [HttpGet]
    public async Task<ActionResult<List<Genre>>> GetAllGenres()
    {
        var genres = await libraryService.GetAllGenres();
        return genres;
    }

    [Route(nameof(CreateGenre))]
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre([FromBody]CreateGenreDto createGenreDto)
    {
        var result = await libraryService.CreateGenre(createGenreDto);
        return result;
    }
}
