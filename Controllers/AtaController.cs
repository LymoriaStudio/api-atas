// Controllers/AtaController.cs
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
public class AtaController : ControllerBase
{
    private readonly AppDbContext _context;

    public AtaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AtaResponseDto>>> GetAll()
    {
        var atas = await _context.Atas
            .Include(a => a.Departamento)
            .Include(a => a.Responsavel)
            .Include(a => a.Participantes)
                .ThenInclude(ap => ap.Pessoa)
            .ToListAsync();

        var response = atas.Select(a => new AtaResponseDto
        {
            Id = a.Id,
            Titulo = a.Titulo,
            Tipo = a.Tipo,
            DataCriacao = a.DataCriacao,
            Status = a.Status,
            ArquivoUrl = a.ArquivoUrl,
            NumeroAta = a.NumeroAta,
            DataReuniao = a.DataReuniao,
            Departamento = a.Departamento.Nome,
            Responsavel = a.Responsavel.Nome,
            Participantes = a.Participantes.Select(ap => ap.Pessoa.Nome).ToList()
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AtaResponseDto>> GetById(int id)
    {
        var ata = await _context.Atas
            .Include(a => a.Departamento)
            .Include(a => a.Responsavel)
            .Include(a => a.Participantes)
                .ThenInclude(ap => ap.Pessoa)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (ata is null) return NotFound();

        var response = new AtaResponseDto
        {
            Id = ata.Id,
            Titulo = ata.Titulo,
            Tipo = ata.Tipo,
            DataCriacao = ata.DataCriacao,
            Status = ata.Status,
            ArquivoUrl = ata.ArquivoUrl,
            NumeroAta = ata.NumeroAta,
            DataReuniao = ata.DataReuniao,
            Departamento = ata.Departamento.Nome,
            Responsavel = ata.Responsavel.Nome,
            Participantes = ata.Participantes.Select(ap => ap.Pessoa.Nome).ToList()
        };

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AtaResponseDto>> Create(AtaRequestDto dto)
    {
        var departamento = await _context.Departamentos.FindAsync(dto.DepartamentoId);
        if (departamento is null) return BadRequest("Departamento não encontrado.");

        var responsavel = await _context.Pessoas.FindAsync(dto.ResponsavelId);
        if (responsavel is null) return BadRequest("Responsável não encontrado.");

        var participantes = await _context.Pessoas
            .Where(p => dto.ParticipantesIds.Contains(p.Id))
            .ToListAsync();

        var ata = new Ata
        {
            Titulo = dto.Titulo,
            Tipo = dto.Tipo,
            DataCriacao = DateTime.UtcNow,
            Status = dto.Status,
            ArquivoUrl = dto.ArquivoUrl,
            NumeroAta = dto.NumeroAta,
            DataReuniao = dto.DataReuniao,
            DepartamentoId = dto.DepartamentoId,
            ResponsavelId = dto.ResponsavelId,
            Participantes = participantes.Select(p => new AtaParticipante { PessoaId = p.Id }).ToList()
        };

        _context.Atas.Add(ata);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ata.Id }, new AtaResponseDto
        {
            Id = ata.Id,
            Titulo = ata.Titulo,
            Tipo = ata.Tipo,
            DataCriacao = ata.DataCriacao,
            Status = ata.Status,
            ArquivoUrl = ata.ArquivoUrl,
            NumeroAta = ata.NumeroAta,
            DataReuniao = ata.DataReuniao,
            Departamento = departamento.Nome,
            Responsavel = responsavel.Nome,
            Participantes = participantes.Select(p => p.Nome).ToList()
        });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AtaRequestDto dto)
    {
        var ata = await _context.Atas
            .Include(a => a.Participantes)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (ata is null) return NotFound();

        var participantes = await _context.Pessoas
            .Where(p => dto.ParticipantesIds.Contains(p.Id))
            .ToListAsync();

        ata.Titulo = dto.Titulo;
        ata.Tipo = dto.Tipo;
        ata.DataCriacao = DateTime.UtcNow;
        ata.Status = dto.Status;
        ata.ArquivoUrl = dto.ArquivoUrl;
        ata.NumeroAta = dto.NumeroAta;
        ata.DataReuniao = dto.DataReuniao;
        ata.DepartamentoId = dto.DepartamentoId;
        ata.ResponsavelId = dto.ResponsavelId;
        ata.Participantes = participantes.Select(p => new AtaParticipante { AtaId = id, PessoaId = p.Id }).ToList();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ata = await _context.Atas.FindAsync(id);
        if (ata is null) return NotFound();

        _context.Atas.Remove(ata);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}