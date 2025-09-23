using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateGenreRequestDto
{
    [Required] [MinLength(1)] public string GenreId { get; set; }
    [Required] [MinLength(1)] public string Name { get; set; }
    [Required] [MinLength(1)] public List<string> BooksIds { get; set; }
}