namespace apiatas.Models;
using Microsoft.EntityFrameworkCore;

public class Departamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ICollection<Ata> Atas { get; set; } = [];
}