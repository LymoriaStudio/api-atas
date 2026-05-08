// DTOs/Pessoa/PessoaResponseDto.cs
using Microsoft.EntityFrameworkCore;
namespace apiatas.DTO.Pessoa;
public class PessoaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Cargo {get;set;}
}