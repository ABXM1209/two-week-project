using api.Dtos;
using api.Dtos.Requests;

namespace api.Services;

public interface ILibraryService
{
    Task<List<AuthorDto>> GetAllAuthors();
    Task<List<BookDto>> GetAllBooks();
    Task<List<GenreDto>> GetAllGenres();
    Task<AuthorDto> CreateAuthor(CreateAuthorRequestDto dto);
    Task<AuthorDto> UpdateAuthor(UpdateAuthorRequestDto dto);
    Task<AuthorDto> DeleteAuthor(string id);
    Task<BookDto> CreateBook(CreateBookRequestDto dto);
    Task<BookDto> UpdateBook(UpdateBookRequestDto dto);
    Task<BookDto> DeleteBook(string id);
    Task<GenreDto> CreateGenre(CreateGenreRequestDto dto);
    Task<GenreDto> UpdateGenre(UpdateGenreRequestDto dto);
    Task<GenreDto> DeleteGenre(string id);
}
