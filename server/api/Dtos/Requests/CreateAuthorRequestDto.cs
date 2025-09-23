using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public record CreateAuthorRequestDto
{
    [Required] [MinLength(1)] public string Name { get; set; }
}