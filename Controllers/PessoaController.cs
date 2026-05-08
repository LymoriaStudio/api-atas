// Controllers/PessoaController.cs
namespace apiatas.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiatas.Data;
using apiatas.Models;
using apiatas.DTO.Ata;
using apiatas.DTO.Departamento;
using apiatas.DTO.Pessoa;
[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly AppDbContext _context;

    public PessoaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<PessoaResponseDto>>> GetAll()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        return Ok(pessoas.Select(p => new PessoaResponseDto { Id = p.Id, Nome = p.Nome, Email = p.Email }));
    }

    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> Create(PessoaRequestDto dto)
    {
        var pessoa = new Pessoa { Nome = dto.Nome, Email = dto.Email };
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new PessoaResponseDto { Id = pessoa.Id, Nome = pessoa.Nome, Email = pessoa.Email });
    }
}