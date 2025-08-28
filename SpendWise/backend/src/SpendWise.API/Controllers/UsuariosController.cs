using Microsoft.AspNetCore.Mvc;
using SpendWise.Application.DTOs;
using SpendWise.Application.Commands.Usuario;
using SpendWise.Application.Queries.Usuario;
using MediatR;

namespace SpendWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obter todos os usuários
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
    {
        var query = new GetAllUsuariosQuery();
        var usuarios = await _mediator.Send(query);
        return Ok(usuarios);
    }

    /// <summary>
    /// Obter usuário por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetById(Guid id)
    {
        var query = new GetUsuarioByIdQuery { Id = id };
        var usuario = await _mediator.Send(query);
        
        if (usuario == null)
            return NotFound();
            
        return Ok(usuario);
    }

    /// <summary>
    /// Criar novo usuário
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create([FromBody] CreateUsuarioDto createDto)
    {
        var command = new CreateUsuarioCommand
        {
            Nome = createDto.Nome,
            Email = createDto.Email,
            Password = createDto.Password,
            RendaMensal = createDto.RendaMensal
        };

        var usuario = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    /// <summary>
    /// Atualizar usuário
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioDto>> Update(Guid id, [FromBody] UpdateUsuarioDto updateDto)
    {
        var command = new UpdateUsuarioCommand
        {
            Id = id,
            Nome = updateDto.Nome,
            RendaMensal = updateDto.RendaMensal
        };

        var usuario = await _mediator.Send(command);
        
        if (usuario == null)
            return NotFound();
            
        return Ok(usuario);
    }

    /// <summary>
    /// Deletar usuário
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteUsuarioCommand { Id = id };
        var success = await _mediator.Send(command);
        
        if (!success)
            return NotFound();
            
        return NoContent();
    }
}
