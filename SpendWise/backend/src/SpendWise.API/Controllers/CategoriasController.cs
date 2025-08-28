using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Application.Commands.Categorias;
using SpendWise.Application.DTOs;
using SpendWise.Application.Queries.Categorias;

namespace SpendWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
    {
        var query = new GetAllCategoriasQuery();
        var categorias = await _mediator.Send(query);
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDto>> GetById(Guid id)
    {
        var query = new GetCategoriaByIdQuery(id);
        var categoria = await _mediator.Send(query);
        
        if (categoria == null)
            return NotFound();
            
        return Ok(categoria);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetByUsuario(Guid usuarioId)
    {
        var query = new GetCategoriasByUsuarioQuery(usuarioId);
        var categorias = await _mediator.Send(query);
        return Ok(categorias);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Create([FromBody] CreateCategoriaCommand command)
    {
        var categoria = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoriaDto>> Update(Guid id, [FromBody] UpdateCategoriaCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var categoria = await _mediator.Send(command);
        
        if (categoria == null)
            return NotFound();
            
        return Ok(categoria);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteCategoriaCommand(id);
        var result = await _mediator.Send(command);
        
        if (!result)
            return NotFound();
            
        return NoContent();
    }
}