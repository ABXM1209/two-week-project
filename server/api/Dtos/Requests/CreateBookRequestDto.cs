using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public record CreateBookRequestDto
{
    [Required][Range(24,int.MaxValue)] public int Pages { get; set; }
    [Required] [MinLength(1)] public string Title { get; set; }
}