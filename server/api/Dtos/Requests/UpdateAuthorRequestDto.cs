using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateAuthorRequestDto
{
    [Required] [MinLength(1)] public string AuthorId { get; set; }
    [Required] [MinLength(1)] public string Name { get; set; }
    [Required] [MinLength(1)] public List<string> BooksIds { get; set; }
}