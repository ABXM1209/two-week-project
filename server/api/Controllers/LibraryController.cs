using System.ComponentModel.DataAnnotations;
using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class LibraryController(ILibraryService libraryService) : ControllerBase
{
    [Route(nameof(GetAllAuthors))]
    [HttpGet]
    public async Task<List<AuthorDto>> GetAllAuthors()
    {
        return await libraryService.GetAllAuthors();
    }   
    
    [Route(nameof(GetAllBooks))]
    [HttpGet]
    public async Task<List<BookDto>> GetAllBooks()
    {
        return await libraryService.GetAllBooks();
    }  
    
    [Route(nameof(GetAllGenres))]
    [HttpGet]
    public async Task<List<GenreDto>> GetAllGenres()
    {
        return await libraryService.GetAllGenres();
    }

    [Route(nameof(CreateAuthor))]
    [HttpPost]
    public async Task<AuthorDto> CreateAuthor([FromBody]CreateAuthorRequestDto dto)
    {
        return await libraryService.CreateAuthor(dto);
    }
    
    [HttpPost(nameof(CreateBook))]
    public async Task<BookDto> CreateBook([FromBody]CreateBookRequestDto dto)
    {
        return await libraryService.CreateBook(dto);
    }
    
    [Route(nameof(CreateGenre))]
    [HttpPost]
    public async Task<GenreDto> CreateGenre([FromBody]CreateGenreRequestDto dto)
    {
        var result = await libraryService.CreateGenre(dto);
        return result;
    }

    [HttpPut(nameof(UpdateAuthor))]
    public async Task<AuthorDto> UpdateAuthor([FromBody] UpdateAuthorRequestDto dto)
    {
        return await libraryService.UpdateAuthor(dto);
    }
    
    [HttpPut(nameof(UpdateBook))]
    public async Task<BookDto> UpdateBook([FromBody]UpdateBookRequestDto dto)
    {
        return await libraryService.UpdateBook(dto);
    }

    [HttpPut(nameof(UpdateGenre))]
    public async Task<GenreDto> UpdateGenre([FromBody] UpdateGenreRequestDto dto)
    {
        return await libraryService.UpdateGenre(dto);
    }

    [HttpDelete(nameof(DeleteAuthor))]
    public async Task<AuthorDto> DeleteAuthor([FromQuery] string authorId)
    {
        return await libraryService.DeleteAuthor(authorId);
    }
    
    [HttpDelete(nameof(DeleteBook))]
    public async Task<BookDto> DeleteBook([FromQuery] string bookId)
    {
        return await libraryService.DeleteBook(bookId);
    }

    [HttpDelete(nameof(DeleteGenre))]
    public async Task<GenreDto> DeleteGenre([FromQuery] string genreId)
    {
        return await libraryService.DeleteGenre(genreId);
    }
}
