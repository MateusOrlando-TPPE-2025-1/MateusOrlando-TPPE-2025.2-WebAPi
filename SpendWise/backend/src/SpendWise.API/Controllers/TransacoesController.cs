using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Application.Commands.Transacoes;
using SpendWise.Application.DTOs;
using SpendWise.Application.Queries.Transacoes;

namespace SpendWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransacoesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetAll()
    {
        var query = new GetAllTransacoesQuery();
        var transacoes = await _mediator.Send(query);
        return Ok(transacoes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransacaoDto>> GetById(Guid id)
    {
        var query = new GetTransacaoByIdQuery(id);
        var transacao = await _mediator.Send(query);

        if (transacao == null)
            return NotFound();

        return Ok(transacao);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetByUsuario(Guid usuarioId)
    {
        var query = new GetTransacoesByUsuarioQuery(usuarioId);
        var transacoes = await _mediator.Send(query);
        return Ok(transacoes);
    }

    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetByCategoria(Guid categoriaId)
    {
        var query = new GetTransacoesByCategoriaQuery(categoriaId);
        var transacoes = await _mediator.Send(query);
        return Ok(transacoes);
    }

    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Create([FromBody] CreateTransacaoCommand command)
    {
        var transacao = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = transacao.Id }, transacao);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransacaoDto>> Update(Guid id, [FromBody] UpdateTransacaoCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var transacao = await _mediator.Send(command);

        if (transacao == null)
            return NotFound();

        return Ok(transacao);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteTransacaoCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }
    
    [HttpGet("periodo")]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetByPeriodo(
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim,
        [FromQuery] Guid? usuarioId = null)
    {
        var query = new GetTransacoesByPeriodoQuery(dataInicio, dataFim, usuarioId);
        var transacoes = await _mediator.Send(query);
        return Ok(transacoes);
    }
}