namespace apiatas.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiatas.Data;
using apiatas.Models;
using apiatas.DTO.Ata;
using apiatas.DTO.Departamento;
using apiatas.DTO.Pessoa;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartamentoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartamentoResponseDto>>> GetAll()
    {
        var departamentos = await _context.Departamentos.ToListAsync();
        return Ok(departamentos.Select(d => new DepartamentoResponseDto { Id = d.Id, Nome = d.Nome }));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<DepartamentoResponseDto>> Create(DepartamentoRequestDto dto)
    {
        var departamento = new Departamento { Nome = dto.Nome };
        _context.Departamentos.Add(departamento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new DepartamentoResponseDto { Id = departamento.Id, Nome = departamento.Nome });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dep = await _context.Departamentos.FindAsync(id);
        if(dep is null)
        {
            return NotFound("Departamento Não encontrado");
        }

        _context.Departamentos.Remove(dep);

        return NoContent();
    }

}