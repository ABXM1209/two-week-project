using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public record CreateGenreRequestDto
{
    [Required] [MinLength(1)] public string Name { get; set; }
}