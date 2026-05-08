// DTOs/Ata/AtaRequestDto.cs
namespace apiatas.DTO.Ata;
using Microsoft.EntityFrameworkCore;
public class AtaRequestDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ArquivoUrl { get; set; }
    public string? NumeroAta { get; set; }
    public DateTime? DataReuniao { get; set; }
    public int DepartamentoId { get; set; }
    public int ResponsavelId { get; set; }
    public List<int> ParticipantesIds { get; set; } = [];
}