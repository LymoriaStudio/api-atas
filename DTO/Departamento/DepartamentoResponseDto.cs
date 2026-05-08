// DTOs/Departamento/DepartamentoResponseDto.cs
namespace apiatas.DTO.Departamento;
using Microsoft.EntityFrameworkCore;
public class DepartamentoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}