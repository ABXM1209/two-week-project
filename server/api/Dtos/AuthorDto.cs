using efscaffold.Entities;
namespace api.Dtos;
public class AuthorDto
{
    public AuthorDto(Author entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Createdat = entity.Createdat;
        BooksIds = entity.Books?.Select(b =>b.Id).ToList() ?? new List<string>();
    }
    
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime? Createdat { get; set; }
    public virtual ICollection<string> BooksIds { get; set; } = new List<string>();
}