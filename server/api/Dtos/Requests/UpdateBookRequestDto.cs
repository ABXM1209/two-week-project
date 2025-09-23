using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateBookRequestDto
{
    [Required] [MinLength(1)] public string BookId { get; set; }
    [Required] [Range(24, Int32.MaxValue)]public int Pages { get; set; }
    [Required] [MinLength(1)] public string Title { get; set; }
    [Required] [MinLength(1)] public List<string> AuthorsIds { get; set; }
    [Required] [MinLength(1)] public string GenreId { get; set; }
}