namespace apiatas.Models;
using Microsoft.EntityFrameworkCore;


public class Ata
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ArquivoUrl { get; set; }

    public string? NumeroAta { get; set; }
    public DateTime? DataReuniao { get; set; }

    public int DepartamentoId { get; set; }
    public Departamento Departamento { get; set; } = null!;

    public int ResponsavelId { get; set; }
    public Pessoa Responsavel { get; set; } = null!;

    public ICollection<AtaParticipante> Participantes { get; set; } = [];
}