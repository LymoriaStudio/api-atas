namespace apiatas.Models;
using Microsoft.EntityFrameworkCore;

public class AtaParticipante
{
    public int AtaId { get; set; }
    public Ata Ata { get; set; } = null!;

    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
}