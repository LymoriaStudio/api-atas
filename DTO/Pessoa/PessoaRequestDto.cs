// DTOs/Pessoa/PessoaRequestDto.cs
namespace apiatas.DTO.Pessoa;

using Microsoft.EntityFrameworkCore;
public class PessoaRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; }
}