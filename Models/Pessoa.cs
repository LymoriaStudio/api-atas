using Microsoft.EntityFrameworkCore;
namespace apiatas.Models;


public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Cargo {get;set;}

    public ICollection<AtaParticipante> AtasParticipadas { get; set; } = [];
}