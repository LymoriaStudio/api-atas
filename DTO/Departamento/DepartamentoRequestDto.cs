// DTOs/Departamento/DepartamentoRequestDto.cs
using Microsoft.EntityFrameworkCore;
namespace apiatas.DTO.Departamento;
public class DepartamentoRequestDto
{
    public string Nome { get; set; } = string.Empty;
}