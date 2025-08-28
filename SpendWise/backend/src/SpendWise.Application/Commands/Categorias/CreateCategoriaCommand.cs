using MediatR;
using SpendWise.Application.DTOs;
using SpendWise.Domain.Enums;

namespace SpendWise.Application.Commands.Categorias;

public record CreateCategoriaCommand(
    string Nome,
    string? Descricao,
    TipoCategoria Tipo,
    Guid UsuarioId
) : IRequest<CategoriaDto>;