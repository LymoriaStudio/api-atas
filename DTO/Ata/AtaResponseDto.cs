// DTOs/Ata/AtaResponseDto.cs
using Microsoft.EntityFrameworkCore;
namespace apiatas.DTO.Ata;
public class AtaResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ArquivoUrl { get; set; }
    public string? NumeroAta { get; set; }
    public DateTime? DataReuniao { get; set; }
    public string Departamento { get; set; } = string.Empty;
    public string Responsavel { get; set; } = string.Empty;
    public List<string> Participantes { get; set; } = [];
}